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

namespace Gibbed.IO
{
    public static partial class NumberHelpers
    {
        public static Int16 BigEndian(this Int16 value)
        {
            if (BitConverter.IsLittleEndian == true)
            {
                return value.Swap();
            }

            return value;
        }

        public static UInt16 BigEndian(this UInt16 value)
        {
            if (BitConverter.IsLittleEndian == true)
            {
                return value.Swap();
            }

            return value;
        }

        public static Int32 BigEndian(this Int32 value)
        {
            if (BitConverter.IsLittleEndian == true)
            {
                return value.Swap();
            }

            return value;
        }

        public static UInt32 BigEndian(this UInt32 value)
        {
            if (BitConverter.IsLittleEndian == true)
            {
                return value.Swap();
            }

            return value;
        }

        public static Int64 BigEndian(this Int64 value)
        {
            if (BitConverter.IsLittleEndian == true)
            {
                return value.Swap();
            }

            return value;
        }

        public static UInt64 BigEndian(this UInt64 value)
        {
            if (BitConverter.IsLittleEndian == true)
            {
                return value.Swap();
            }

            return value;
        }

        public static Single BigEndian(this Single value)
        {
            if (BitConverter.IsLittleEndian == true)
            {
                var data = BitConverter.GetBytes(value);
                var junk = BitConverter.ToUInt32(data, 0).Swap();
                return BitConverter.ToSingle(BitConverter.GetBytes(junk), 0);
            }

            return value;
        }

        public static Double BigEndian(this Double value)
        {
            if (BitConverter.IsLittleEndian == true)
            {
                var data = BitConverter.GetBytes(value);
                var junk = BitConverter.ToUInt64(data, 0).Swap();
                return BitConverter.ToDouble(BitConverter.GetBytes(junk), 0);
            }

            return value;
        }
    }
}
