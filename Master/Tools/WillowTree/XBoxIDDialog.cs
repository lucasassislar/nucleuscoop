using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using X360.STFS;
using X360.IO;

namespace WillowTree
{
    public partial class XBoxIDDialog : Form
    {
        public XBoxUniqueID ID;

        public XBoxIDDialog()
        {
            InitializeComponent();
        }
   
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog tempOpen = new OpenFileDialog();
            tempOpen.DefaultExt = "*.sav";
            tempOpen.Filter = "WillowSaveGame(*.sav)|*.sav";

            if (tempOpen.ShowDialog() == DialogResult.OK)
            {
                XBoxIDFilePath.Text = tempOpen.FileName;
                try
                {
                    ID = new XBoxUniqueID(tempOpen.FileName);
                    ProfileBox.Text = ID.ProfileID.ToString("X");
                    DeviceBox.Text = BitConverter.ToString(ID.DeviceID);
                    DeviceBox.Text = DeviceBox.Text.Replace("-", "");
                }
                catch
                {
                    ID = null;
                    MessageBox.Show("The file is not a valid Xbox 360 savegame file.");
                    return;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ID != null)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Please select a valid Xbox 360 save to use first.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

    }
    public class XBoxUniqueID
    {
        public long ProfileID { get; private set; }
        public byte[] DeviceID { get; private set; }

        public XBoxUniqueID(string FileName)
        {
            BinaryReader br = new BinaryReader(File.Open(FileName, FileMode.Open), Encoding.ASCII);
            string Magic = new string(br.ReadChars(3));
            if (Magic != "CON")
            {
                throw new FileFormatException();
            }
            br.Close();
            br = null;

            STFSPackage CON = new STFSPackage(new DJsIO(FileName, DJFileMode.Open, true), new X360.Other.LogRecord());
            ProfileID = CON.Header.ProfileID;
            DeviceID = CON.Header.DeviceID;
            CON.CloseIO();
        }
    }
}
