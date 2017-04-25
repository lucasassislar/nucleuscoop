using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Nucleus.Gaming
{
    public static class DInputManager
    {
        private static DInputLibrary[] libraries = new DInputLibrary[]
            {
                new DInputLibrary()
                {
                    Hash1 = 0x70E51E05F1448047,//8134941312786137159,
                    Hash2 = 0x76665852371D3A39,//8531603654235208249,
                    ID = 1
                },
            };


        public class DInputLibrary
        {
            public long Hash1;
            public long Hash2;
            public int ID;
        };

        private static DInputLibrary library;
        public static DInputLibrary Library { get { return library; } }

        static DInputManager()
        {
            bool is64OS = Environment.Is64BitOperatingSystem;
            string path;
            if (is64OS)
            {
                path = @"C:\Windows\SysWow64\dinput.dll";
            }
            else
            {
                // no freaking idea where it really is
                path = @"C:\Windows\System32\dinput.dll";
            }

            byte[] data = File.ReadAllBytes(path);
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(data);

            long a = BitConverter.ToInt64(hash, 0);
            long b = BitConverter.ToInt64(hash, 8);

            DInputLibrary def = libraries.FirstOrDefault(c => c.Hash1 == a && c.Hash2 == b);
            if (def != null)
            {
                library = def;
            }
            else
            {
                throw new NotImplementedException("Your dinput.dll is not in the database!");
            }
        }
    }
}
