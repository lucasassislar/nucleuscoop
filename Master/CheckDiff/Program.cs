using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CheckDiff
{
    class Program
    {
        static Dictionary<string, string> fileData = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            string folder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            DirectoryInfo dir = new DirectoryInfo(folder);
            RecursiveAddFolder(dir);

            Console.WriteLine("Press Enter when ready to compare...");
            Console.ReadLine();

            RecursiveTestFolder(dir);

            Console.WriteLine();
            Console.WriteLine("End of data");
            Console.ReadLine();
        }

        private static void RecursiveAddFolder(DirectoryInfo dir)
        {
            FileInfo[] files = dir.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo f = files[i];
                try
                {
                    using (Stream s = f.OpenRead())
                    {
                        // MD5 check-sum
                        using (var md5 = MD5.Create())
                        {
                            string m = BitConverter.ToString(md5.ComputeHash(s)).Replace("-", "").ToLower();
                            fileData.Add(f.FullName, m);
                        }
                    }
                }
                catch { }
            }

            DirectoryInfo[] d = dir.GetDirectories();
            for (int i =0; i < d.Length; i++)
            {
                DirectoryInfo da = d[i];
                if (da.Name.ToLower() == "common")
                {
                    Console.WriteLine("Skipped common folder: " + da.FullName);
                    continue;
                }

                RecursiveAddFolder(da);
            }
        }
        private static void RecursiveTestFolder(DirectoryInfo dir)
        {
            FileInfo[] files = dir.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo f = files[i];
                string original;
                if (fileData.TryGetValue(f.FullName, out original))
                {
                    using (Stream s = f.OpenRead())
                    {
                        // MD5 check-sum
                        using (var md5 = MD5.Create())
                        {
                            string m = BitConverter.ToString(md5.ComputeHash(s)).Replace("-", "").ToLower();
                            if (m != original)
                            {
                                Console.WriteLine("File " + f.Name + " is different - "  + f.FullName);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("File " + f.Name + " is new - " + f.FullName);
                }
            }
        }
    }
}
