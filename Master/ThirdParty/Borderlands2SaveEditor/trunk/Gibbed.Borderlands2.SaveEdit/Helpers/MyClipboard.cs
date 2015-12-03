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
using System.Collections.Specialized;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Gibbed.Borderlands2.SaveEdit
{
    internal static class MyClipboard
    {
        #region TryAgain
        private const int _Tries = 5;
        private const int _Interval = 100;

        public enum Result
        {
            Success = 0,
            Failure,
        }

        private static bool IsClipboardErrorCode(uint errorCode)
        {
            return errorCode == 0x800401D0u /* CLIPBRD_E_CANT_OPEN */||
                   errorCode == 0x800401D1u /* CLIPBRD_E_CANT_EMPTY */||
                   errorCode == 0x800401D2u /* CLIPBRD_E_CANT_SET */||
                   errorCode == 0x800401D3u /* CLIPBRD_E_BAD_DATA */||
                   errorCode == 0x800401D4u /* CLIPBRD_E_CANT_CLOSE */;
        }

        private static Result TryAgain(Action callback)
        {
            for (int i = 0;; i++)
            {
                try
                {
                    callback();
                    return Result.Success;
                }
                catch (COMException e)
                {
                    if (IsClipboardErrorCode((uint)e.ErrorCode) == true)
                    {
                        if (i < _Tries)
                        {
                            Thread.Sleep(_Interval);
                        }
                        else
                        {
                            return Result.Failure;
                        }
                    }
                    else
                    {
                        throw e;
                    }
                }
            }
        }

        private static Result TryAgain<TResult>(Func<TResult> callback, out TResult result)
        {
            for (int i = 0;; i++)
            {
                try
                {
                    result = callback();
                    return Result.Success;
                }
                catch (COMException e)
                {
                    if (IsClipboardErrorCode((uint)e.ErrorCode) == true)
                    {
                        if (i < _Tries)
                        {
                            Thread.Sleep(_Interval);
                        }
                        else
                        {
                            result = default(TResult);
                            return Result.Failure;
                        }
                    }
                    else
                    {
                        throw e;
                    }
                }
            }
        }
        #endregion

        public static Result Clear()
        {
            return TryAgain(Clipboard.Clear);
        }

        public static Result ContainsAudio(out bool result)
        {
            return TryAgain(Clipboard.ContainsAudio, out result);
        }

        public static Result ContainsData(string format, out bool result)
        {
            return TryAgain(() => Clipboard.ContainsData(format), out result);
        }

        public static Result ContainsFileDropList(out bool result)
        {
            return TryAgain(Clipboard.ContainsFileDropList, out result);
        }

        public static Result ContainsImage(out bool result)
        {
            return TryAgain(Clipboard.ContainsImage, out result);
        }

        public static Result ContainsText(out bool result)
        {
            return TryAgain(Clipboard.ContainsText, out result);
        }

        public static Result ContainsText(TextDataFormat format, out bool result)
        {
            return TryAgain(() => Clipboard.ContainsText(format), out result);
        }

        public static Result GetAudioStream(out Stream result)
        {
            return TryAgain(Clipboard.GetAudioStream, out result);
        }

        public static Result GetData(string format, out object result)
        {
            return TryAgain(() => Clipboard.GetData(format), out result);
        }

        public static Result GetDataObject(out IDataObject result)
        {
            return TryAgain(Clipboard.GetDataObject, out result);
        }

        public static Result GetFileDropList(out StringCollection result)
        {
            return TryAgain(Clipboard.GetFileDropList, out result);
        }

        public static Result GetImage(out BitmapSource result)
        {
            return TryAgain(Clipboard.GetImage, out result);
        }

        public static Result GetText(out string result)
        {
            return TryAgain(Clipboard.GetText, out result);
        }

        public static Result GetText(TextDataFormat format, out string result)
        {
            return TryAgain(() => Clipboard.GetText(format), out result);
        }

        public static Result IsCurrent(IDataObject data, out bool result)
        {
            return TryAgain(() => Clipboard.IsCurrent(data), out result);
        }

        public static Result SetAudio(byte[] audioBytes)
        {
            return TryAgain(() => Clipboard.SetAudio(audioBytes));
        }

        public static Result SetAudio(Stream audioStream)
        {
            return TryAgain(() => Clipboard.SetAudio(audioStream));
        }

        public static Result SetData(string format, object data)
        {
            return TryAgain(() => Clipboard.SetData(format, data));
        }

        public static Result SetDataObject(object data)
        {
            return TryAgain(() => Clipboard.SetDataObject(data));
        }

        public static Result SetDataObject(object data, bool copy)
        {
            return TryAgain(() => Clipboard.SetDataObject(data, copy));
        }

        public static Result SetFileDropList(StringCollection fileDropList)
        {
            return TryAgain(() => Clipboard.SetFileDropList(fileDropList));
        }

        public static Result SetImage(BitmapSource image)
        {
            return TryAgain(() => Clipboard.SetImage(image));
        }

        public static Result SetText(string text)
        {
            return TryAgain(() => Clipboard.SetText(text));
        }

        public static Result SetText(string text, TextDataFormat format)
        {
            return TryAgain(() => Clipboard.SetText(text, format));
        }
    }
}
