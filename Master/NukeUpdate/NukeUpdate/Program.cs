using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Update
{
    class Program
    {
        private static string UpdatesFolder = @"D:\Projects\SplitPlay\Update";

        static void Main(string[] args)
        {
            DirectoryInfo dir = new DirectoryInfo("update");
            RecursiveGetAllFiles(dir);

            DirectoryInfo updatesFolder = new DirectoryInfo(UpdatesFolder);
            var files = updatesFolder.GetFiles().OrderByDescending(h => h.CreationTime);
            string old = files.First().FullName;

            DateTime now = DateTime.Now;
            CultureInfo c = CultureInfo.InvariantCulture;
            string updateString = now.Day.ToString(c) + "_" + now.Month.ToString(c) + "_" + now.Year.ToString(c) + "_" + now.Hour + "_" + now.Minute;
            string updateFile = updateString + ".up";
            updateFile = Path.Combine(UpdatesFolder, updateFile);
            string newUpdateFolder = Path.Combine(UpdatesFolder, updateString);
            Directory.CreateDirectory(newUpdateFolder);

            if (string.IsNullOrEmpty(old)) // dont have old file
            {
                using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(updateFile)))
                {
                    writer.Write(now.ToBinary());
                    writer.Write(allFiles.Count);

                    for (int i = 0; i < allFiles.Count; i++)
                    {
                        FileInfo file = allFiles[i];

                        using (Stream stream = file.OpenRead())
                        {
                            using (var md5 = MD5.Create())
                            {
                                string hashed = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                                string relativePath = file.FullName.Replace(dir.FullName + @"\", "");

                                string newPath = Path.Combine(newUpdateFolder, relativePath);
                                file.CopyTo(newPath);

                                writer.Write(hashed);
                                writer.Write(relativePath);
                                writer.Write(updateString);
                                writer.Write(false);
                            }
                        }
                    }
                }
            }
            else
            {
                // open old file
                using (BinaryReader reader = new BinaryReader(File.OpenRead(old)))
                {
                    // analyze
                    DateTime time = DateTime.FromBinary(reader.ReadInt64());
                    int all = reader.ReadInt32();

                    for (int i = 0; i < all; i++)
                    {
                        string hash = reader.ReadString();
                        string path = reader.ReadString();
                        string lastVersion = reader.ReadString();
                        bool deleted = reader.ReadBoolean();

                        FileInfo file;
                        if (dic.TryGetValue(path.ToLower(), out file))
                        {
                            // the file exists on the last update

                            // hash our current version
                            string newHash = Hash(file);
                            if (newHash != hash)
                            {
                                // file has new values
                                FileData data = new FileData();
                                data.deleted = false;
                                data.relativePath = path;
                                data.latestVersion = updateString;
                                data.hash = hash;
                                fileData.Add(data);

                                string newPath = Path.Combine(newUpdateFolder, path);

                                if (File.Exists(newPath))
                                {
                                    File.Delete(newPath);
                                }
                                file.CopyTo(newPath);
                            }
                            else
                            {
                                // file didn't change
                                FileData data = new FileData();
                                data.deleted = false;
                                data.relativePath = path;
                                data.latestVersion = lastVersion;
                                data.hash = hash;
                                fileData.Add(data);
                            }
                        }
                        else
                        {
                            // the file doesn't exist anymore
                            FileData data = new FileData();
                            data.deleted = true;
                            data.relativePath = path;
                            data.latestVersion = updateString;
                            data.hash = hash;
                            fileData.Add(data);
                        }
                    }
                }

                // look for new files
                for (int i = 0; i < allFiles.Count; i++)
                {
                    FileInfo file = allFiles[i];
                    string relativePath = file.FullName.Replace(dir.FullName + @"\", "");

                    if (!HasPath(relativePath))
                    {
                        // file is new
                        FileData data = new FileData();
                        data.deleted = false;
                        data.relativePath = relativePath;
                        data.latestVersion = updateString;
                        data.hash = Hash(file);
                        fileData.Add(data);

                        string newPath = Path.Combine(newUpdateFolder, relativePath);
                        file.CopyTo(newPath);
                    }
                }

                using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(updateFile)))
                {
                    writer.Write(now.ToBinary());
                    writer.Write(fileData.Count);

                    for (int i = 0; i < fileData.Count; i++)
                    {
                        FileData file = fileData[i];

                        writer.Write(file.hash);
                        writer.Write(file.relativePath);
                        writer.Write(file.latestVersion);
                        writer.Write(file.deleted);
                    }
                }

            }
        }

        private static string Hash(FileInfo file)
        {
            using (Stream stream = file.OpenRead())
            {
                using (var md5 = MD5.Create())
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                }
            }
        }

        private static List<FileInfo> allFiles = new List<FileInfo>();
        private static Dictionary<string, FileInfo> dic = new Dictionary<string, FileInfo>();
        private static List<FileData> fileData = new List<FileData>();

        private static bool HasPath(string path)
        {
            for (int i = 0; i < fileData.Count; i++)
            {
                if (fileData[i].relativePath == path)
                {
                    return true;
                }
            }
            return false;
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
