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
    public class BitReader
    {
        private byte[] _Buffer;
        public int Length { get; private set; }
        public int Position { get; set; }

        private void Initialize(byte[] buffer, int offset, int length)
        {
            this._Buffer = new byte[length];
            Array.Copy(buffer, offset, this._Buffer, 0, length);
            this.Length = length * 8;
            this.Position = 0;
        }

        public BitReader(byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            this.Initialize(buffer, 0, buffer.Length);
        }

        public BitReader(byte[] buffer, int offset, int length)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            if (offset < 0 ||
                offset > buffer.Length)
            {
                throw new ArgumentOutOfRangeException("offset");
            }

            if (length < 0 ||
                offset + length < 0 ||
                offset + length > buffer.Length)
            {
                throw new ArgumentOutOfRangeException("length");
            }

            this.Initialize(buffer, offset, length);
        }

        public short ReadInt16(int bits)
        {
            if (bits < 0 || bits > 16)
            {
                throw new ArgumentOutOfRangeException("bits");
            }

            return (short)this.ReadUInt32(bits);
        }

        public ushort ReadUInt16(int bits)
        {
            if (bits < 0 || bits > 16)
            {
                throw new ArgumentOutOfRangeException("bits");
            }

            return (ushort)this.ReadUInt32(bits);
        }

        public int ReadInt32(int bits)
        {
            return (int)this.ReadUInt32(bits);
        }

        public uint ReadUInt32(int bits)
        {
            if (bits < 0 || bits > 32)
            {
                throw new ArgumentOutOfRangeException("bits");
            }

            uint result = 0;
            int shift = 0;
            while (bits > 0)
            {
                if (this.Position >= this.Length)
                {
                    throw new InvalidOperationException();
                }

                int offset = this.Position % 8;
                int left = Math.Min(8 - offset, bits);

                var mask = (uint)(1 << left) - 1;
                var value = (uint)(this._Buffer[this.Position >> 3] >> offset);

                bits -= left;

                result |= (mask & value) << shift;
                shift += left;

                this.Position += left;
            }
            return result;
        }

        public long ReadInt64(int bits)
        {
            return (long)this.ReadUInt64(bits);
        }

        public ulong ReadUInt64(int bits)
        {
            if (bits < 0 || bits > 64)
            {
                throw new ArgumentOutOfRangeException("bits");
            }

            if (bits <= 32)
            {
                return this.ReadUInt32(bits);
            }

            ulong result = 0;
            int shift = 0;
            while (bits > 0)
            {
                if (this.Position >= this.Length)
                {
                    throw new InvalidOperationException();
                }

                int offset = this.Position % 8;
                int left = Math.Min(8 - offset, bits);

                var mask = (ulong)(1 << left) - 1;
                var value = (ulong)(this._Buffer[this.Position >> 3] >> offset);

                bits -= left;

                result |= (mask & value) << shift;
                shift += left;

                this.Position += left;
            }
            return result;
        }

        public bool ReadBoolean()
        {
            return this.ReadUInt32(1) != 0;
        }
    }
}
