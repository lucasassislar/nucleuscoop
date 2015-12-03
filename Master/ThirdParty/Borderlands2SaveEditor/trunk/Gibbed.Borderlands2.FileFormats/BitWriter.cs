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

namespace Gibbed.Borderlands2.FileFormats
{
    public class BitWriter
    {
        private byte[] _Buffer;

        public int AllocatedLength
        {
            get { return this._Buffer.Length * 8; }
        }

        public int Position { get; set; }

        public BitWriter()
        {
            this._Buffer = new byte[0];
            this.Position = 0;
        }

        private void Resize(int newSize)
        {
            var start = this._Buffer.Length;
            Array.Resize(ref this._Buffer, newSize);
            if (newSize > start)
            {
                for (int i = start; i < this._Buffer.Length; i++)
                {
                    this._Buffer[i] = 0xFF;
                }
            }
        }

        public byte[] GetBuffer()
        {
            var length = (this.Position + 7) >> 3;
            var buffer = new byte[length];
            Array.Copy(this._Buffer, buffer, length);
            return buffer;
        }

        public void WriteInt16(short value, int bits)
        {
            if (bits < 0 || bits > 16)
            {
                throw new ArgumentOutOfRangeException("bits");
            }

            this.WriteUInt32((uint)value, bits);
        }

        public void WriteUInt16(ushort value, int bits)
        {
            if (bits < 0 || bits > 16)
            {
                throw new ArgumentOutOfRangeException("bits");
            }

            this.WriteUInt32(value, bits);
        }

        public void WriteInt32(int value, int bits)
        {
            this.WriteUInt32((uint)value, bits);
        }

        public void WriteUInt32(uint value, int bits)
        {
            if (bits < 0 || bits > 32)
            {
                throw new ArgumentOutOfRangeException("bits");
            }

            while (bits > 0)
            {
                if (this.Position >= this.AllocatedLength)
                {
                    this.Resize(128);
                }

                int offset = this.Position % 8;
                int left = Math.Min(8 - offset, bits);

                uint mask = (1u << left) - 1;
                this._Buffer[this.Position >> 3] &= (byte)(~(mask << offset));
                this._Buffer[this.Position >> 3] |= (byte)((value & mask) << offset);
                bits -= left;
                value >>= left;
                this.Position += left;
            }
        }

        public void WriteInt64(long value, int bits)
        {
            this.WriteUInt64((ulong)value, bits);
        }

        public void WriteUInt64(ulong value, int bits)
        {
            if (bits < 0 || bits > 64)
            {
                throw new ArgumentOutOfRangeException("bits");
            }

            while (bits > 0)
            {
                if (this.Position >= this.AllocatedLength)
                {
                    Array.Resize(ref this._Buffer, this._Buffer.Length + 128);
                }

                int offset = this.Position % 8;
                int left = Math.Min(8 - offset, bits);

                ulong mask = (1ul << left) - 1;
                this._Buffer[this.Position >> 3] |= (byte)((value & mask) << offset);
                bits -= left;
                value >>= left;
                this.Position += left;
            }
        }

        public void WriteBoolean(bool value)
        {
            this.WriteUInt32(value == true ? 1u : 0u, 1);
        }
    }
}
