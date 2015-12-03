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
        #region ReadValueS16
        public static Int16 ReadValueS16(this Stream stream)
        {
            return stream.ReadValueS16(Endian.Little);
        }

        public static Int16 ReadValueS16(this Stream stream, Endian endian)
        {
            var data = stream.ReadBytes(2);
            var value = BitConverter.ToInt16(data, 0);

            if (ShouldSwap(endian) == true)
            {
                value = value.Swap();
            }

            return value;
        }
        #endregion

        #region WriteValueS16
        public static void WriteValueS16(this Stream stream, Int16 value)
        {
            stream.WriteValueS16(value, Endian.Little);
        }

        public static void WriteValueS16(this Stream stream, Int16 value, Endian endian)
        {
            if (ShouldSwap(endian) == true)
            {
                value = value.Swap();
            }

            var data = BitConverter.GetBytes(value);
            Debug.Assert(data.Length == 2);
            stream.WriteBytes(data);
        }
        #endregion

        #region Obsolete
        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Int16 ReadValueS16(this Stream stream, bool littleEndian)
        {
            return stream.ReadValueS16(littleEndian == true ? Endian.Little : Endian.Big);
        }

        [Obsolete("use Endian enum instead of boolean to represent endianness")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void WriteValueS16(this Stream stream, Int16 value, bool littleEndian)
        {
            stream.WriteValueS16(value, littleEndian == true ? Endian.Little : Endian.Big);
        }
        #endregion
    }
}
