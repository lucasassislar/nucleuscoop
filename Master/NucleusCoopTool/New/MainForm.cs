using Nucleus.Gaming;
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

namespace Nucleus.Coop
{
    public partial class MainForm : BaseForm
    {
        private GameManager gameManager;

        public MainForm()
        {
            InitializeComponent();

            gameManager = new GameManager();
            if (gameManager.User.Games.Count == 0)
            {
                if (MessageBox.Show("You have no games on your list. Would you like to automatically search your disk?", "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ScanExes();
                }
            }
        }

        public void ScanExes()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            for (int i = 0; i < drives.Length; i++)
            {
                DriveInfo d = drives[i];

                if (d.DriveFormat != "NTFS")
                {
                    continue;
                }

                LogManager.Log("> Searching drive {0} for game executables", d.Name);

                Dictionary<ulong, FileNameAndParentFrn> mDict = new Dictionary<ulong, FileNameAndParentFrn>();
                MFTReader mft = new MFTReader();
                mft.Drive = d.RootDirectory.FullName;

                try
                {
                    mft.EnumerateVolume(out mDict, new string[] { ".exe" });
                    foreach (KeyValuePair<UInt64, FileNameAndParentFrn> entry in mDict)
                    {
                        FileNameAndParentFrn file = (FileNameAndParentFrn)entry.Value;

                        string name = file.Name;
                        string lower = name.ToLower();

                        GameInfo game;
                        if (gameManager.GameInfos.TryGetValue(lower, out game))
                        {
                            string path = mft.GetFullPath(file);
                            LogManager.Log("Found game: {0}, full path: {1}", game.GameName, path);

                            UserGameInfo info = new UserGameInfo();
                            info.InitializeDefault(game, path);
                            gameManager.User.Games.Add(info);
                        }
                    }
                }
                catch
                {
                    // disc is probably not NTFS
                }
            }

            gameManager.SaveUserProfile();
            gameManager.WaitSave();
        }
    }
}
