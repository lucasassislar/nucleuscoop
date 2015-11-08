using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Nucleus.Gaming
{
    public enum SymbolicLink
    {
        File = 0,
        Directory = 1
    }
    public static class FileUtil
    {
        [DllImport("kernel32.dll")]
        public static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, SymbolicLink dwFlags);

        

        public static void Write(byte[] data, string place)
        {
            using (MemoryStream str = new MemoryStream(data))
            {
                using (FileStream stream = new FileStream(place, FileMode.Create))
                {
                    str.CopyTo(stream);
                    stream.Flush();
                }
            }
        }
    }
}
