using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.PkgMaker
{
    public partial class MainForm : BaseForm
    {
        public MainForm()
        {
            InitializeComponent();

            //RepositoryHeader header = new RepositoryHeader();
            //header.PackagesInfoRoot = "infos";
            //header.PackagesRoot = "packages";

            //List<RepositoryGameInfo> games = new List<RepositoryGameInfo>();

            //RepositoryGameInfo l4d2 = new RepositoryGameInfo();
            //l4d2.ExeName = "left4dead2";
            //l4d2.ID = "550";
            //l4d2.Title = "Left 4 Dead 2";
            //l4d2.Developer = "distrolucas";
            //games.Add(l4d2);



            //header.Games = games.ToArray();
            // save store header
            string root = @"C:\Web\";
            string headerPath = Path.Combine(root, "header.json");
            string infosRoot = Path.Combine(root, "infos");
            string packagesRoot = Path.Combine(root, "packages");

            Directory.CreateDirectory(infosRoot);
            Directory.CreateDirectory(packagesRoot);

            //string headerTxt = JsonConvert.SerializeObject(header);
            //if (File.Exists(headerPath))
            //{
            //    File.Delete(headerPath);
            //}
            //File.WriteAllText(headerPath, headerTxt);

            // make full descriptions
            //StoreGameFullInfo l4d2full = new StoreGameFullInfo();
            //Fill(l4d2full, l4d2);
            //l4d2full.Description = "Handler for Left 4 Dead 2";
            //l4d2full.PlatformVersion = 9; // alpha 9
            //l4d2full.Version = 1;
            //WriteInfo(l4d2full, infosRoot);

            //ThreadPool.QueueUserWorkItem(QueueClose);
        }

        void QueueClose(object state)
        {
            Thread.Sleep(100);
            Action cl = Close;
            Invoke(cl);
        }

        //private void WriteInfo(StoreGameFullInfo cl, string path)
        //{
        //    WriteClass(cl, Path.Combine(path, cl.ID + ".json"));
        //}

        private void WriteClass(object cl, string path)
        {
            string txt = JsonConvert.SerializeObject(cl);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.WriteAllText(path, txt);
        }

        //private void Fill(StoreGameFullInfo full, StoreGameInfo info)
        //{
        //    full.ID = info.ID;
        //    full.ExeName = info.ExeName;
        //    full.Developer = info.Developer;
        //    full.Title = info.Title;
        //}
    }
}
