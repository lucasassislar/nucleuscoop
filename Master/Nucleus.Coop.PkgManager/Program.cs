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
    public class Infos
    {
        public GameHandlerMetadata Metadata;
        public string RootFolder;
    }

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
            string infosFolder = Path.Combine(output, "infos");
            string pkgsFolder = Path.Combine(output, "packages");

            string toBuild = "..\\..\\MainRepo\\packages\\sources";

            string indexPagePath = Path.Combine(output, "index.html");
            string indexData = "<html><head><link rel='stylesheet' href='bootstrap.css'><link rel='stylesheet' href='style.css'></head><body>";

            if (Directory.Exists(output))
            {
                Directory.Delete(output, true);
            }

            Directory.CreateDirectory(output);
            Directory.CreateDirectory(infosFolder);
            Directory.CreateDirectory(pkgsFolder);

            string sourceCssPath = "..\\..\\MainRepo\\bootstrap.min.css";
            string cssPath = Path.Combine(output, "bootstrap.css");
            File.Copy(sourceCssPath, cssPath);

            sourceCssPath = "..\\..\\MainRepo\\style.css";
            cssPath = Path.Combine(output, "style.css");
            File.Copy(sourceCssPath, cssPath);

            DirectoryInfo dirSource = new DirectoryInfo(toBuild);
            DirectoryInfo[] dirs = dirSource.GetDirectories();

            List<Infos> infos = new List<Infos>();

            for (int i = 0; i < dirs.Length; i++)
            {
                DirectoryInfo dir = dirs[i];

                // read handler data
                string handlerPath = Path.Combine(dir.FullName, "info.json");
                string handlerData = File.ReadAllText(handlerPath);
                GameHandlerMetadata metadata = JsonConvert.DeserializeObject<GameHandlerMetadata>(handlerData);
                if (metadata.GameID.ToLower().StartsWith("debug"))
                {
                    continue;
                }

                string pkgName = PackageManager.GetPackageFileName(metadata) + ".nc";
                string destName = Path.Combine(pkgsFolder, pkgName);

                infos.Add(new Infos() { Metadata = metadata, RootFolder = dir.FullName });

                string firstPic = Path.Combine(dir.FullName, "header.jpg");
                if (!File.Exists(firstPic))
                {
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

            var newInfos = infos.OrderBy(c => c.Metadata.GameTitle);

            indexData += "<div id='grid'>";

            foreach (Infos info in newInfos)
            {
                GameHandlerMetadata metadata = info.Metadata;

                // write info file
                GameHandlerBaseMetadata baseMetadata = new GameHandlerBaseMetadata();
                ObjectUtil.DeepCopy(metadata, baseMetadata);

                string metadataSerialized = JsonConvert.SerializeObject(baseMetadata);
                string infoFolder = Path.Combine(infosFolder, metadata.HandlerID);
                Directory.CreateDirectory(infoFolder);

                string infoFile = Path.Combine(infoFolder, "info.json");
                File.WriteAllText(infoFile, metadataSerialized);

                string firstPic = Path.Combine(info.RootFolder, "header.jpg");
                if (File.Exists(firstPic))
                {
                    string destHeaderScreenshot = Path.Combine(infoFolder, "header.jpg");
                    Directory.CreateDirectory(Path.GetDirectoryName(destHeaderScreenshot));
                    File.Copy(firstPic, destHeaderScreenshot);
                }

                indexData += "<div>";

                string pkgName = PackageManager.GetPackageFileName(metadata);

                indexData += string.Format("<a href='packages/{0}.nc'>", pkgName);
                indexData += string.Format("<img src='infos/{0}/header.jpg' /></a> ", metadata.HandlerID);
                indexData += string.Format("<h3>{0}</h3><h4>{1}</h4><h5><a href='packages/{2}.nc'>[DOWNLOAD HANDLER v{3}]</a></h5>", 
                    metadata.GameTitle, metadata.Title, pkgName, metadata.V);
                indexData += "<br /> </div>";
            }

            indexData += "</div>";


            indexData += "</body></html>";
            File.WriteAllText(indexPagePath, indexData);


            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}
