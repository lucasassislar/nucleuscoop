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
using System.Text;

namespace Gibbed.IO
{
    public class ByteBufferReader
    {
        private readonly byte[] _Buffer;
        private readonly Endian _Endian;
        private readonly int _Offset;

        public ByteBufferReader(byte[] buffer)
            : this(buffer, 0, Endian.Little)
        {
        }

        public ByteBufferReader(byte[] buffer, int offset)
            : this(buffer, offset, Endian.Little)
        {
        }

        public ByteBufferReader(byte[] buffer, int offset, Endian endian)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            if (offset < 0 || offset >= buffer.Length)
            {
                throw new ArgumentOutOfRangeException("offset");
            }

            this._Buffer = buffer;
            this._Offset = offset;
            this._Endian = endian;
        }

        public ByteBufferReader this[int index]
        {
            get
            {
                if (index == 0)
                {
                    return this;
                }

                return new ByteBufferReader(this._Buffer, this._Offset + index, this._Endian);
            }
        }

        public bool ReadValueB8()
        {
            return this._Buffer[this._Offset] != 0;
        }

        public bool ReadValueB32()
        {
            // no need for swap
            return BitConverter.ToInt32(this._Buffer, this._Offset) != 0;
        }

        public sbyte ReadValueS8()
        {
            return (sbyte)this._Buffer[this._Offset];
        }

        public byte ReadValueU8()
        {
            return this._Buffer[this._Offset];
        }

        public short ReadValueS16()
        {
            var value = BitConverter.ToInt16(this._Buffer, this._Offset);

            if (StreamHelpers.ShouldSwap(this._Endian) == true)
            {
                value = value.Swap();
            }

            return value;
        }

        public ushort ReadValueU16()
        {
            var value = BitConverter.ToUInt16(this._Buffer, this._Offset);

            if (StreamHelpers.ShouldSwap(this._Endian) == true)
            {
                value = value.Swap();
            }

            return value;
        }

        public int ReadValueS32()
        {
            var value = BitConverter.ToInt32(this._Buffer, this._Offset);

            if (StreamHelpers.ShouldSwap(this._Endian) == true)
            {
                value = value.Swap();
            }

            return value;
        }

        public uint ReadValueU32()
        {
            var value = BitConverter.ToUInt32(this._Buffer, this._Offset);

            if (StreamHelpers.ShouldSwap(this._Endian) == true)
            {
                value = value.Swap();
            }

            return value;
        }

        public long ReadValueS64()
        {
            var value = BitConverter.ToInt64(this._Buffer, this._Offset);

            if (StreamHelpers.ShouldSwap(this._Endian) == true)
            {
                value = value.Swap();
            }

            return value;
        }

        public ulong ReadValueU64()
        {
            var value = BitConverter.ToUInt64(this._Buffer, this._Offset);

            if (StreamHelpers.ShouldSwap(this._Endian) == true)
            {
                value = value.Swap();
            }

            return value;
        }

        public float ReadValueF32()
        {
            if (StreamHelpers.ShouldSwap(this._Endian) == true)
            {
                return
                    BitConverter.ToSingle(
                        BitConverter.GetBytes(BitConverter.ToInt32(this._Buffer, this._Offset).Swap()), 0);
            }

            return BitConverter.ToSingle(this._Buffer, this._Offset);
        }

        public double ReadValueF64()
        {
            if (StreamHelpers.ShouldSwap(this._Endian) == true)
            {
                return BitConverter.Int64BitsToDouble(BitConverter.ToInt64(this._Buffer, this._Offset).Swap());
            }

            return BitConverter.ToDouble(this._Buffer, this._Offset);
        }

        public string ReadString(int size, bool trailingNull, Encoding encoding)
        {
            if (size < 0 || this._Offset + size > this._Buffer.Length)
            {
                throw new ArgumentOutOfRangeException("size");
            }

            string value = encoding.GetString(this._Buffer, this._Offset, size);

            if (trailingNull == true)
            {
                var position = value.IndexOf('\0');
                if (position >= 0)
                {
                    value = value.Substring(0, position);
                }
            }

            return value;
        }
    }
}
