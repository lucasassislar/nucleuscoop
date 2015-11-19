using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetFolderFileList
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            string location = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            DirectoryInfo dir = new DirectoryInfo(location);
            string txt = "";
            DirectoryInfo[] dirs = dir.GetDirectories();
            for (int i = 0; i < dirs.Length; i++)
            {
                DirectoryInfo d = dirs[i];
                //txt += "mklink /d \"" + d.Name + "\" \"" + d.FullName + "\"" + Environment.NewLine;
                txt += "\"mklink /d \\\"\" + Path.Combine(l4dBinFolder, \"" + d.Name + "\") + \"\\\"  \\\"\" + Path.Combine(l4dFolder, \"" + d.Name + "\") + \"\\\"\"," + Environment.NewLine;
            }
            FileInfo[] files = dir.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo d = files[i];
                //txt += "mklink \"" + f.Name + "\" \"" + f.FullName + "\"" + Environment.NewLine;
                txt += "\"mklink \\\"\" + Path.Combine(l4dBinFolder, \"" + d.Name + "\") + \"\\\"  \\\"\" + Path.Combine(l4dFolder, \"" + d.Name + "\") + \"\\\"\"," + Environment.NewLine;
            }
            textBox1.Text = txt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
