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
        public static Int16 RotateRight(this Int16 value, int count)
        {
            return (Int16)(((UInt16)value).RotateRight(count));
        }

        public static UInt16 RotateRight(this UInt16 value, int count)
        {
            return (UInt16)((value >> count) | (value << (16 - count)));
        }

        public static Int32 RotateRight(this Int32 value, int count)
        {
            return (Int32)(((UInt32)value).RotateRight(count));
        }

        public static UInt32 RotateRight(this UInt32 value, int count)
        {
            return (value >> count) | (value << (32 - count));
        }

        public static Int64 RotateRight(this Int64 value, int count)
        {
            return (Int64)(((UInt64)value).RotateRight(count));
        }

        public static UInt64 RotateRight(this UInt64 value, int count)
        {
            return (value >> count) | (value << (64 - count));
        }
    }
}
