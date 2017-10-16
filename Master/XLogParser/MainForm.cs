using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XLogParser
{
    public partial class MainForm : Form
    {
        private string logPath;
        private List<int> unique;

        public MainForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Log files|*.log";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    logPath = open.FileName;
                    ProcessLog();
                }
            }
        }

        private void ProcessLog()
        {
            unique = new List<int>();
            txtUnique.Text = "";
            txtParsed.Text = "";

            string[] lines = File.ReadAllLines(logPath);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (!line.Contains("MESSAGE"))
                {
                    continue;
                }

                string[] splitted = line.Split(':');
                string last = splitted.Last();

                int code = int.Parse(last);
                //Messages win = (Messages)code;

                //if (!unique.Contains(code))
                //{
                //    unique.Add(code);
                //    txtUnique.Text += win.ToString() + Environment.NewLine;
                //}

                //txtParsed.Text += win.ToString() + Environment.NewLine;
            }
        }
    }
}
