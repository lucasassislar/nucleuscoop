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
using System.IO;

namespace Gibbed.IO
{
    public static partial class StreamHelpers
    {
        internal static bool ShouldSwap(Endian endian)
        {
            switch (endian)
            {
                case Endian.Little: return BitConverter.IsLittleEndian == false;
                case Endian.Big: return BitConverter.IsLittleEndian == true;
                default: throw new ArgumentException("unsupported endianness", "endian");
            }
        }

        public static MemoryStream ReadToMemoryStream(this Stream stream, long size, int buffer)
        {
            var memory = new MemoryStream();

            long left = size;
            var data = new byte[buffer];
            while (left > 0)
            {
                var block = (int)(Math.Min(left, data.Length));
                if (stream.Read(data, 0, block) != block)
                {
                    throw new EndOfStreamException();
                }
                memory.Write(data, 0, block);
                left -= block;
            }

            memory.Seek(0, SeekOrigin.Begin);
            return memory;
        }

        public static MemoryStream ReadToMemoryStream(this Stream stream, long size)
        {
            return stream.ReadToMemoryStream(size, 0x40000);
        }

        public static void WriteFromStream(this Stream stream, Stream input, long size, int buffer)
        {
            long left = size;
            var data = new byte[buffer];
            while (left > 0)
            {
                var block = (int)(Math.Min(left, data.Length));
                if (input.Read(data, 0, block) != block)
                {
                    throw new EndOfStreamException();
                }
                stream.Write(data, 0, block);
                left -= block;
            }
        }

        public static void WriteFromStream(this Stream stream, Stream input, long size)
        {
            stream.WriteFromStream(input, size, 0x40000);
        }

        public static byte[] ReadBytes(this Stream stream, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length");
            }

            var data = new byte[length];
            var read = stream.Read(data, 0, length);
            if (read != length)
            {
                throw new EndOfStreamException();
            }

            return data;
        }

        public static byte[] ReadBytes(this Stream stream, uint length)
        {
            return stream.ReadBytes((int)length);
        }

        public static void WriteBytes(this Stream stream, byte[] data)
        {
            stream.Write(data, 0, data.Length);
        }
    }
}
