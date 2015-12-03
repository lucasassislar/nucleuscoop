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
        public static Encoding DefaultEncoding = Encoding.ASCII;

        public static string ReadString(this Stream stream, uint size)
        {
            return stream.ReadStringInternalStatic(DefaultEncoding, size, false);
        }

        public static string ReadString(this Stream stream, uint size, bool trailingNull)
        {
            return stream.ReadStringInternalStatic(DefaultEncoding, size, trailingNull);
        }

        public static string ReadStringZ(this Stream stream)
        {
            return stream.ReadStringInternalDynamic(DefaultEncoding, '\0');
        }

        public static void WriteString(this Stream stream, string value)
        {
            stream.WriteStringInternalStatic(DefaultEncoding, value);
        }

        public static void WriteString(this Stream stream, string value, uint size)
        {
            stream.WriteStringInternalStatic(DefaultEncoding, value, size);
        }

        public static void WriteStringZ(this Stream stream, string value)
        {
            stream.WriteStringInternalDynamic(DefaultEncoding, value, '\0');
        }
    }
}
