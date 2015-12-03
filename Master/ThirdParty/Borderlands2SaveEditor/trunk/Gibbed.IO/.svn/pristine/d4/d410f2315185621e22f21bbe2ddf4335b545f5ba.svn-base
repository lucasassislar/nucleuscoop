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
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace Gibbed.IO
{
    public static partial class StreamHelpers
    {
        #region ReadValueGuid
        public static Guid ReadValueGuid(this Stream stream, Endian endian)
        {
            var a = stream.ReadValueS32(endian);
            var b = stream.ReadValueS16(endian);
            var c = stream.ReadValueS16(endian);
            var d = stream.ReadBytes(8);
            return new Guid(a, b, c, d);
        }

        public static Guid ReadValueGuid(this Stream stream)
        {
            return stream.ReadValueGuid(Endian.Little);
        }
        #endregion

        #region WriteValueGuid
        public static void WriteValueGuid(this Stream stream, Guid value, Endian endian)
        {
            var data = value.ToByteArray();
            Debug.Assert(data.Length == 16);
            stream.WriteValueS32(BitConverter.ToInt32(data, 0), endian);
            stream.WriteValueS16(BitConverter.ToInt16(data, 4), endian);
            stream.WriteValueS16(BitConverter.ToInt16(data, 6), endian);
            stream.Write(data, 8, 8);
        }

        public static void WriteValueGuid(this Stream stream, Guid value)
        {
            stream.WriteValueGuid(value, Endian.Little);
        }
        #endregion

        #region Obsolete
        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Guid ReadValueGuid(this Stream stream, bool littleEndian)
        {
            return stream.ReadValueGuid(littleEndian == true ? Endian.Little : Endian.Big);
        }

        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        public static void WriteValueGuid(this Stream stream, Guid value, bool littleEndian)
        {
            stream.WriteValueGuid(value, littleEndian == true ? Endian.Little : Endian.Big);
        }
        #endregion
    }
}
