using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TempBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            DirectoryInfo dirInfo = new DirectoryInfo(directory);
            //dirInfo.Create();

            List<FileInfo> files = dirInfo.GetFiles("*.dll").ToList();
            files.AddRange(dirInfo.GetFiles("*.pdb"));
            files.AddRange(dirInfo.GetFiles("*.xml"));

            Console.WriteLine($"Files {files.Count}");

            string binDir = Path.Combine(directory, "bin");

            for (int i = 0; i < files.Count; i++)
            {
                FileInfo file = files[i];
                string prefix = $"({i + 1}/{files.Count}) ";
                Console.WriteLine(prefix + $"Moving {file.Name}");

                string destination = Path.Combine(binDir, file.Name);
                if (File.Exists(destination))
                {
                    File.Delete(destination);
                }

                file.MoveTo(destination);
            }
        }
    }
}
