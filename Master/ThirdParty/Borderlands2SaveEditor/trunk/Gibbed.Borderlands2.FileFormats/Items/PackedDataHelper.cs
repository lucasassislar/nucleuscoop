/* Copyright (c) 2012 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.Linq;
using Gibbed.Borderlands2.GameInfo;
using Gibbed.IO;

namespace Gibbed.Borderlands2.FileFormats.Items
{
    public abstract class PackedDataHelper<TWeapon, TItem>
        where TWeapon : BaseWeapon, new()
        where TItem : BaseItem, new()
    {
        public static byte[] Encode(IPackable packable)
        {
            if (packable == null)
            {
                throw new ArgumentNullException("packable");
            }

            if ((packable is BaseWeapon) == false &&
                (packable is BaseItem) == false)
            {
                throw new ArgumentException("unsupported packable");
            }

            var assetLibrarySet =
                InfoManager.AssetLibraryManager.Sets.SingleOrDefault(s => s.Id == packable.AssetLibrarySetId);
            if (assetLibrarySet == null)
            {
                throw new ArgumentException();
            }

            var writer = new BitWriter();
            writer.WriteInt32(InfoManager.AssetLibraryManager.Version, 7);
            writer.WriteBoolean(packable is BaseWeapon);
            writer.WriteInt32(packable.UniqueId, 32);
            writer.WriteUInt16(0xFFFF, 16);
            writer.WriteInt32(packable.AssetLibrarySetId, 8);
            packable.Write(writer);

            var unobfuscatedBytes = writer.GetBuffer();
            if (unobfuscatedBytes.Length > 40)
            {
                throw new InvalidOperationException();
            }

            if (unobfuscatedBytes.Length < 40)
            {
                var start = unobfuscatedBytes.Length;
                Array.Resize(ref unobfuscatedBytes, 40);
                for (int i = start; i < unobfuscatedBytes.Length; i++)
                {
                    unobfuscatedBytes[i] = 0xFF;
                }
            }

            var hash = CRC32.Hash(unobfuscatedBytes, 0, unobfuscatedBytes.Length);
            var computedCheck = (ushort)(((hash & 0xFFFF0000) >> 16) ^ ((hash & 0x0000FFFF) >> 0));

            unobfuscatedBytes[5] = (byte)((computedCheck & 0xFF00) >> 8);
            unobfuscatedBytes[6] = (byte)((computedCheck & 0x00FF) >> 0);

            for (int i = unobfuscatedBytes.Length - 1; i > 5; i--)
            {
                if (unobfuscatedBytes[i] != 0xFF)
                {
                    Array.Resize(ref unobfuscatedBytes, i + 1);
                    break;
                }
            }

            var data = (byte[])unobfuscatedBytes.Clone();
            var seed = BitConverter.ToUInt32(data, 1).Swap();
            BogoEncrypt(seed, data, 5, data.Length - 5);
            return data;
        }

        private static void BogoEncrypt(uint seed, byte[] buffer, int offset, int length)
        {
            var temp = new byte[length];

            var rightHalf = (int)((seed % 32) % length);
            var leftHalf = length - rightHalf;

            Array.Copy(buffer, offset, temp, leftHalf, rightHalf);
            Array.Copy(buffer, offset + rightHalf, temp, 0, leftHalf);

            var xor = (uint)((int)seed >> 5);
            for (int i = 0; i < length; i++)
            {
                xor = (uint)(((ulong)xor * 0x10A860C1UL) % 0xFFFFFFFBUL);
                temp[i] ^= (byte)(xor & 0xFF);
            }

            Array.Copy(temp, 0, buffer, offset, length);
        }

        public static IPackable Decode(byte[] data)
        {
            if (data.Length < 5 || data.Length > 40)
            {
                throw new ArgumentOutOfRangeException("data");
            }

            var seed = BitConverter.ToUInt32(data, 1).Swap();
            var unobfuscatedBytes = (byte[])data.Clone();
            BogoDecrypt(seed, unobfuscatedBytes, 5, unobfuscatedBytes.Length - 5);

            var fileCheck = BitConverter.ToUInt16(unobfuscatedBytes, 5).Swap();

            unobfuscatedBytes[5] = 0xFF;
            unobfuscatedBytes[6] = 0xFF;

            if (unobfuscatedBytes.Length < 40)
            {
                var start = unobfuscatedBytes.Length;
                Array.Resize(ref unobfuscatedBytes, 40);
                for (int i = start; i < unobfuscatedBytes.Length; i++)
                {
                    unobfuscatedBytes[i] = 0xFF;
                }
            }

            var hash = CRC32.Hash(unobfuscatedBytes, 0, unobfuscatedBytes.Length);
            var computedCheck = (ushort)(((hash & 0xFFFF0000) >> 16) ^ ((hash & 0x0000FFFF) >> 0));

            if (fileCheck != computedCheck)
            {
                throw new FormatException();
            }

            var reader = new BitReader(unobfuscatedBytes);

            var version = reader.ReadInt32(7);
            if (version != InfoManager.AssetLibraryManager.Version)
            {
                throw new FormatException();
            }

            var isWeapon = reader.ReadBoolean();

            int uniqueId = 0;
            if (version >= 3)
            {
                uniqueId = reader.ReadInt32(32);
            }

            ushort check = reader.ReadUInt16(16);
            if (check != 0xFFFF)
            {
                throw new FormatException();
            }

            int setId = 0;
            if (version >= 2)
            {
                setId = reader.ReadInt32(8);
            }

            var set = InfoManager.AssetLibraryManager.GetSet(setId);
            if (set == null)
            {
                throw new FormatException();
            }

            if (setId != 0)
            {
            }

            IPackable packable = isWeapon == true
                                     ? (IPackable)new TWeapon()
                                     : new TItem();
            packable.UniqueId = uniqueId;
            packable.AssetLibrarySetId = setId;
            packable.Read(reader);
            return packable;
        }

        private static void BogoDecrypt(uint seed, byte[] buffer, int offset, int length)
        {
            var temp = new byte[length];
            Array.Copy(buffer, offset, temp, 0, length);

            var xor = (uint)((int)seed >> 5);
            for (int i = 0; i < length; i++)
            {
                xor = (uint)(((ulong)xor * 0x10A860C1UL) % 0xFFFFFFFBUL);
                temp[i] ^= (byte)(xor & 0xFF);
            }

            var rightHalf = (int)((seed % 32) % length);
            var leftHalf = length - rightHalf;

            Array.Copy(temp, leftHalf, buffer, offset, rightHalf);
            Array.Copy(temp, 0, buffer, offset + rightHalf, leftHalf);
        }
    }
}
