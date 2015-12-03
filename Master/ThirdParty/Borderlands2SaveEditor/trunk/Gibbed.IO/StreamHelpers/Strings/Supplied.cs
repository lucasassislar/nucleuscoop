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

using System.IO;
using System.Text;

namespace Gibbed.IO
{
    public static partial class StreamHelpers
    {
        public static string ReadString(this Stream stream, uint size, Encoding encoding)
        {
            return stream.ReadStringInternalStatic(encoding, size, false);
        }

        public static string ReadString(this Stream stream, int size, Encoding encoding)
        {
            return stream.ReadStringInternalStatic(encoding, (uint)size, false);
        }

        public static string ReadString(this Stream stream, uint size, bool trailingNull, Encoding encoding)
        {
            return stream.ReadStringInternalStatic(encoding, size, trailingNull);
        }

        public static string ReadString(this Stream stream, int size, bool trailingNull, Encoding encoding)
        {
            return stream.ReadStringInternalStatic(encoding, (uint)size, trailingNull);
        }

        public static string ReadStringZ(this Stream stream, Encoding encoding)
        {
            return stream.ReadStringInternalDynamic(encoding, '\0');
        }

        public static void WriteString(this Stream stream, string value, Encoding encoding)
        {
            stream.WriteStringInternalStatic(encoding, value);
        }

        public static void WriteString(this Stream stream, string value, uint size, Encoding encoding)
        {
            stream.WriteStringInternalStatic(encoding, value, size);
        }

        public static void WriteStringZ(this Stream stream, string value, Encoding encoding)
        {
            stream.WriteStringInternalDynamic(encoding, value, '\0');
        }
    }
}
