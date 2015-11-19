using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Update
{
    class Program
    {
        private static List<FileData> fileData = new List<FileData>();
        private static List<FileInfo> allFiles = new List<FileInfo>();
        private static Dictionary<string, FileInfo> dic = new Dictionary<string, FileInfo>();
        private static readonly string server = "http://splitplay.azurewebsites.net/";

        static void Main(string[] args)
        {
            string updateFile;
            if (args.Length == 0)
            {
                // got to choose the update file from server
                string url = server + "latest.ashx";
                using (WebClient client = new WebClient())
                {
                    string upFileName = client.DownloadString(url);
                    Console.WriteLine("Latest version from server: " + Path.GetFileNameWithoutExtension(upFileName)); 
                    string upFileUrl = server + "update/" + upFileName;
                    Console.WriteLine("Downloading update info..."); 
                    client.DownloadFile(upFileUrl, upFileName);
                    updateFile = upFileName;
                }
            }
            else
            {
                updateFile = args[0];
            }

            string dir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            using (BinaryReader reader = new BinaryReader(File.OpenRead(updateFile)))
            {
                DateTime time = DateTime.FromBinary(reader.ReadInt64());
                int all = reader.ReadInt32();

                for (int i = 0; i < all; i++)
                {
                    FileData data = new FileData();
                    data.hash = reader.ReadString();
                    data.relativePath = reader.ReadString();
                    data.latestVersion = reader.ReadString();
                    data.deleted = reader.ReadBoolean();
                    fileData.Add(data);
                }
            }

            for (int i = 0; i < fileData.Count; i++)
            {
                FileData file = fileData[i];
                string fullPath = Path.Combine(dir, file.relativePath);

                if (file.deleted)
                {
                    // check for file existence locally
                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }

                    Download(file, fullPath);
                }
                else 
                { 
                    if (File.Exists(fullPath))
                    {
                        // compare
                        string hash = Hash(fullPath);

                        if (hash != file.hash)
                        {
                            // new file
                            File.Delete(fullPath);

                        }
                    }
                    else
                    {
                        Download(file, fullPath);
                    }
                }
            }

            Console.WriteLine("Finished updating");
            Console.ReadLine();
        }

        private static void Download(FileData file, string fullPath)
        {
            // download
            try
            {
                Console.WriteLine("Downloading " + file.relativePath);
                string url = server + "update/" + file.latestVersion + "/" + file.relativePath;
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(url, fullPath);
                }
            }
            catch
            {
                Console.WriteLine("Failed downloading file " + file.relativePath);
            }
        }

        private static string Hash(string file)
        {
            using (Stream stream = File.OpenRead(file))
            {
                using (var md5 = MD5.Create())
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                }
            }
        }


        private static void RecursiveGetAllFiles(DirectoryInfo dir)
        {
            FileInfo[] files = dir.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i];
                string relativePath = file.FullName.Replace(dir.FullName + @"\", "");
                dic.Add(relativePath.ToLower(), file);
            }

            allFiles.AddRange(files);


            DirectoryInfo[] dirs = dir.GetDirectories();
            for (int i = 0; i < dirs.Length; i++)
            {
                RecursiveGetAllFiles(dirs[i]);
            }
        }
    }
}
