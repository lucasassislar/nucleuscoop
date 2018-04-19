using Ionic.Zip;
using Newtonsoft.Json;
using Nucleus.Gaming;
using Nucleus.Gaming.Package;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Nucleus.Coop.PkgManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string rootDir = AssemblyUtil.GetStartFolder();
            string output = Path.Combine(rootDir, "output");
            string toBuild = Path.Combine("..\\..\\MainRepo\\packages\\sources");

            Directory.CreateDirectory(output);

            DirectoryInfo dirSource = new DirectoryInfo(toBuild);
            DirectoryInfo[] dirs = dirSource.GetDirectories();

            for (int i = 0; i < dirs.Length; i++)
            {
                DirectoryInfo dir = dirs[i];
                string pkgName = dir.Name + ".nc";
                string destName = Path.Combine(output, pkgName);

                // search for screenshots if missing
                string screenshotsDir = Path.Combine(dir.FullName, "screenshots");
                Directory.CreateDirectory(screenshotsDir);

                string firstPic = Path.Combine(screenshotsDir, "0.jpg");
                if (!File.Exists(firstPic))
                {
                    // read handler data
                    string handlerPath = Path.Combine(dir.FullName, "info.json");
                    string handlerData = File.ReadAllText(handlerPath);
                    GameHandlerMetadata metadata = JsonConvert.DeserializeObject<GameHandlerMetadata>(handlerData);

                    // see if steam has a header pic
                    string headerUrl = string.Format("https://steamcdn-a.opskins.media/steam/apps/{0}/header.jpg", metadata.GameID);

                    try
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.DownloadFile(headerUrl, firstPic);
                        }
                    }
                    catch { }
                }

                using (var file = new ZipFile())
                {
                    file.AddDirectory(dir.FullName);
                    file.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;

                    file.Save(destName);
                }
            }

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}
