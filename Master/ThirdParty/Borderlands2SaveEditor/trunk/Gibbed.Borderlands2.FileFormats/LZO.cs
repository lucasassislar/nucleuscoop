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
using System.Runtime.InteropServices;

namespace Gibbed.Borderlands2.FileFormats
{
    public class LZO
    {
        private static readonly bool _Is64Bit = DetectIs64Bit();

        public enum ErrorCode
        {
            Success = 0,
            GenericError = -1,
            OutOfMemory = -2,
            NotCompressible = -3,
            InputOverrun = -4,
            OutputOverrun = -5,
            LookbehindOverrun = -6,
            EndOfFileNotFound = -7,
            InputNotConsumed = -8,
            NotImplemented = -9,
            InvalidArgument = -10,
        }

        private static bool DetectIs64Bit()
        {
            return Marshal.SizeOf(IntPtr.Zero) == 8;
        }

        private static class Native32
        {
            [DllImport("lzo_32.dll", EntryPoint = "#67", CallingConvention = CallingConvention.StdCall)]
            internal static extern ErrorCode NativeCompress(IntPtr inputBytes,
                                                            int inputCount,
                                                            IntPtr outputBytes,
                                                            ref int outputCount,
                                                            byte[] workBytes);

            [DllImport("lzo_32.dll", EntryPoint = "#68", CallingConvention = CallingConvention.StdCall)]
            internal static extern ErrorCode NativeDecompress(IntPtr inputBytes,
                                                              int inputCount,
                                                              IntPtr outputBytes,
                                                              ref int outputCount);
        }

        private static class Native64
        {
            [DllImport("lzo_64.dll", EntryPoint = "#67", CallingConvention = CallingConvention.StdCall)]
            internal static extern ErrorCode NativeCompress(IntPtr inputBytes,
                                                            int inputCount,
                                                            IntPtr outputBytes,
                                                            ref int outputCount,
                                                            byte[] workBytes);

            [DllImport("lzo_64.dll", EntryPoint = "#68", CallingConvention = CallingConvention.StdCall)]
            internal static extern ErrorCode NativeDecompress(IntPtr inputBytes,
                                                              int inputCount,
                                                              IntPtr outputBytes,
                                                              ref int outputCount);
        }

        private const int _DictSize = 2;
        private const int _WorkSize = (16384 * _DictSize);

        private static readonly byte[] _CompressWork = new byte[_WorkSize];

        public static ErrorCode Compress(byte[] inputBytes,
                                         int inputOffset,
                                         int inputCount,
                                         byte[] outputBytes,
                                         int outputOffset,
                                         ref int outputCount)
        {
            if (inputBytes == null)
            {
                throw new ArgumentNullException("inputBytes");
            }

            if (inputOffset < 0 || inputOffset >= inputBytes.Length)
            {
                throw new ArgumentOutOfRangeException("inputOffset");
            }

            if (inputCount <= 0 || inputOffset + inputCount > inputBytes.Length)
            {
                throw new ArgumentOutOfRangeException("inputCount");
            }

            if (outputBytes == null)
            {
                throw new ArgumentNullException("outputBytes");
            }

            if (outputOffset < 0 || outputOffset >= outputBytes.Length)
            {
                throw new ArgumentOutOfRangeException("outputOffset");
            }

            if (outputCount <= 0 || outputOffset + outputCount > outputBytes.Length)
            {
                throw new ArgumentOutOfRangeException("outputCount");
            }

            var outputHandle = GCHandle.Alloc(outputBytes, GCHandleType.Pinned);
            var inputHandle = GCHandle.Alloc(inputBytes, GCHandleType.Pinned);

            ErrorCode result;

            lock (_CompressWork)
            {
                if (_Is64Bit == true)
                {
                    result = Native64.NativeCompress(inputHandle.AddrOfPinnedObject() + inputOffset,
                                                     inputCount,
                                                     outputHandle.AddrOfPinnedObject() + outputOffset,
                                                     ref outputCount,
                                                     _CompressWork);
                }
                else
                {
                    result = Native32.NativeCompress(inputHandle.AddrOfPinnedObject() + inputOffset,
                                                     inputCount,
                                                     outputHandle.AddrOfPinnedObject() + outputOffset,
                                                     ref outputCount,
                                                     _CompressWork);
                }
            }

            return result;
        }

        public static ErrorCode Decompress(byte[] inputBytes,
                                           int inputOffset,
                                           int inputCount,
                                           byte[] outputBytes,
                                           int outputOffset,
                                           ref int outputCount)
        {
            if (inputBytes == null)
            {
                throw new ArgumentNullException("inputBytes");
            }

            if (inputOffset < 0 || inputOffset >= inputBytes.Length)
            {
                throw new ArgumentOutOfRangeException("inputOffset");
            }

            if (inputCount <= 0 || inputOffset + inputCount > inputBytes.Length)
            {
                throw new ArgumentOutOfRangeException("inputCount");
            }

            if (outputBytes == null)
            {
                throw new ArgumentNullException("outputBytes");
            }

            if (outputOffset < 0 || outputOffset >= outputBytes.Length)
            {
                throw new ArgumentOutOfRangeException("outputOffset");
            }

            if (outputCount <= 0 || outputOffset + outputCount > outputBytes.Length)
            {
                throw new ArgumentOutOfRangeException("outputCount");
            }

            var outputHandle = GCHandle.Alloc(outputBytes, GCHandleType.Pinned);
            var inputHandle = GCHandle.Alloc(inputBytes, GCHandleType.Pinned);

            ErrorCode result;

            if (_Is64Bit == true)
            {
                result = Native64.NativeDecompress(inputHandle.AddrOfPinnedObject() + inputOffset,
                                                   inputCount,
                                                   outputHandle.AddrOfPinnedObject() + outputOffset,
                                                   ref outputCount);
            }
            else
            {
                result = Native32.NativeDecompress(inputHandle.AddrOfPinnedObject() + inputOffset,
                                                   inputCount,
                                                   outputHandle.AddrOfPinnedObject() + outputOffset,
                                                   ref outputCount);
            }

            inputHandle.Free();
            outputHandle.Free();

            return result;
        }
    }
}
