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

namespace Gibbed.IO
{
    public static partial class StreamHelpers
    {
        public static bool ReadValueBoolean(this Stream stream)
        {
            return stream.ReadValueB8();
        }

        public static void WriteValueBoolean(this Stream stream, bool value)
        {
            stream.WriteValueB8(value);
        }

        public static bool ReadValueB8(this Stream stream)
        {
            return stream.ReadValueU8() > 0;
        }

        public static void WriteValueB8(this Stream stream, bool value)
        {
            stream.WriteValueU8((byte)(value == true ? 1 : 0));
        }

        public static bool ReadValueB32(this Stream stream, Endian endian)
        {
            return stream.ReadValueU32(endian) != 0;
        }

        public static bool ReadValueB32(this Stream stream)
        {
            return stream.ReadValueB32(Endian.Little);
        }

        public static void WriteValueB32(this Stream stream, bool value, Endian endian)
        {
            stream.WriteValueU32((byte)(value == true ? 1 : 0), endian);
        }

        public static void WriteValueB32(this Stream stream, bool value)
        {
            stream.WriteValueB32(value, Endian.Little);
        }
    }
}
