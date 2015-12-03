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
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace Gibbed.IO
{
    public static partial class StreamHelpers
    {
        internal static string ReadStringInternalStatic(this Stream stream,
                                                        Encoding encoding,
                                                        uint size,
                                                        bool trailingNull)
        {
            var data = new byte[size];
            stream.Read(data, 0, data.Length);

            string value = encoding.GetString(data, 0, data.Length);

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

        internal static void WriteStringInternalStatic(this Stream stream, Encoding encoding, string value)
        {
            byte[] data = encoding.GetBytes(value);
            stream.Write(data, 0, data.Length);
        }

        internal static void WriteStringInternalStatic(this Stream stream, Encoding encoding, string value, uint size)
        {
            byte[] data = encoding.GetBytes(value);
            Array.Resize(ref data, (int)size);
            stream.Write(data, 0, (int)size);
        }

        internal static string ReadStringInternalDynamic(this Stream stream, Encoding encoding, char end)
        {
            int characterSize = encoding.GetByteCount("e");
            Debug.Assert(characterSize == 1 || characterSize == 2 || characterSize == 4);
            string characterEnd = end.ToString(CultureInfo.InvariantCulture);

            int i = 0;
            var data = new byte[128 * characterSize];

            while (true)
            {
                if (i + characterSize > data.Length)
                {
                    Array.Resize(ref data, data.Length + (128 * characterSize));
                }

                int read = stream.Read(data, i, characterSize);
                Debug.Assert(read == characterSize);

                if (encoding.GetString(data, i, characterSize) == characterEnd)
                {
                    break;
                }

                i += characterSize;
            }

            if (i == 0)
            {
                return "";
            }

            return encoding.GetString(data, 0, i);
        }

        internal static void WriteStringInternalDynamic(this Stream stream, Encoding encoding, string value, char end)
        {
            byte[] data = encoding.GetBytes(value);
            stream.Write(data, 0, data.Length);

            data = encoding.GetBytes(end.ToString(CultureInfo.InvariantCulture));
            stream.Write(data, 0, data.Length);
        }
    }
}
