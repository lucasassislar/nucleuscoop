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
using System.Runtime.InteropServices;

namespace Gibbed.IO
{
    public static partial class StreamHelpers
    {
        public static T ReadStructure<T>(this Stream stream)
        {
            var structureSize = Marshal.SizeOf(typeof(T));
            var buffer = new byte[structureSize];

            if (stream.Read(buffer, 0, structureSize) != structureSize)
            {
                throw new EndOfStreamException("could not read all of data for structure");
            }

            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

            var structure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));

            handle.Free();

            return structure;
        }

        public static T ReadStructure<T>(this Stream stream, int size)
        {
            var structureSize = Marshal.SizeOf(typeof(T));
            var buffer = new byte[Math.Max(structureSize, size)];

            if (stream.Read(buffer, 0, size) != size)
            {
                throw new EndOfStreamException("could not read all of data for structure");
            }

            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

            var structure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));

            handle.Free();

            return structure;
        }

        public static void WriteStructure<T>(this Stream stream, T structure)
        {
            var structureSize = Marshal.SizeOf(typeof(T));
            var buffer = new byte[structureSize];
            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

            Marshal.StructureToPtr(structure, handle.AddrOfPinnedObject(), false);

            handle.Free();

            stream.Write(buffer, 0, buffer.Length);
        }

        public static void WriteStructure<T>(this Stream stream, T structure, int size)
        {
            var structureSize = Marshal.SizeOf(typeof(T));
            var buffer = new byte[Math.Max(structureSize, size)];
            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

            Marshal.StructureToPtr(structure, handle.AddrOfPinnedObject(), false);

            handle.Free();

            stream.Write(buffer, 0, buffer.Length);
        }
    }
}
