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

            List<FileInfo> toDelete = dirInfo.GetFiles("*.pdb").ToList();
            toDelete.AddRange(dirInfo.GetFiles("*.xml"));

            List<FileInfo> files = dirInfo.GetFiles("*.dll").ToList();

            Console.WriteLine($"NucleusCoop Builder Helper");

            string binDir = Path.Combine(directory, "bin");

            Console.WriteLine($"To Delete {files.Count}");
            for (int i = 0; i < toDelete.Count; i++)
            {
                FileInfo file = toDelete[i];
                string prefix = $"({i + 1}/{toDelete.Count}) ";
                Console.WriteLine(prefix + $"Deleting {file.Name}");

                file.Delete();
            }

            Console.WriteLine($"Files {files.Count}");
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
