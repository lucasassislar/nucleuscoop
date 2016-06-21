using Nucleus.Gaming;
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

namespace Nucleus.Coop
{
    public partial class SearchDisksForm : BaseForm
    {
        public struct SearchDriveInfo
        {
            public DriveInfo drive;
            public string text;

            public override string ToString()
            {
                return text;
            }
        }

        public SearchDisksForm()
        {
            InitializeComponent();

            DriveInfo[] drives = DriveInfo.GetDrives();
            CheckedListBox checkedBox = disksBox;

            for (int i = 0; i < drives.Length; i++)
            {
                DriveInfo drive = drives[i];

                SearchDriveInfo d = new SearchDriveInfo();
                d.drive = drive;

                if (drive.IsReady)
                {
                    if (drive.DriveFormat != "NTFS")
                    {
                        // ignore non-NTFS drives
                        continue;
                    }

                    try
                    {
                        long free = drive.AvailableFreeSpace / 1024 / 1024 / 1024;
                        long total = drive.TotalSize / 1024 / 1024 / 1024;
                        long used = total - free;

                        d.text = drive.Name + " " + used + " GB used";
                        checkedBox.Items.Add(d, true);
                    }
                    catch
                    {
                        // notify user of crash
                        d.text = drive.Name + " (Not authorized)";
                        checkedBox.Items.Add(d, CheckState.Indeterminate);
                    }
                }
                else
                {
                    // user might want to get that drive ready
                    d.text = drive.Name + " (Drive not ready)";
                    checkedBox.Items.Add(d, CheckState.Indeterminate);
                }
            }
        }

        private List<SearchDriveInfo> toSearch;
        private float progress;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            toSearch = new List<SearchDriveInfo>();
            CheckedListBox checkedBox = disksBox;

            for (int i = 0; i < checkedBox.CheckedItems.Count; i++)
            {
                SearchDriveInfo info = (SearchDriveInfo)checkedBox.CheckedItems[i];
                toSearch.Add(info);
            }

            Task.Run(new Action(SearchDrives));
        }

        private void SearchDrives()
        {
            for (int i = 0; i < toSearch.Count; i++)
            {
                SearchDrive(i);
            }
        }

        private void UpdateProgress()
        {
            Invoke(new Action(delegate
            {
                progressBar1.Value = Math.Min(100, (int)(progress * 100));
            }));
        }

        private async void SearchDrive(int i)
        {
            SearchDriveInfo info = toSearch[i];
            if (!info.drive.IsReady)
            {
                return;
            }

            LogManager.Log("> Searching drive {0} for game executables", info.drive.Name);

            Dictionary<ulong, FileNameAndParentFrn> mDict = new Dictionary<ulong, FileNameAndParentFrn>();
            MFTReader mft = new MFTReader();
            mft.Drive = info.drive.RootDirectory.FullName;

            await Task.Run(delegate
            {
                mft.EnumerateVolume(out mDict, new string[] { ".exe" });
            });

            progress += (1 / (float)toSearch.Count) / 2.0f;
            UpdateProgress();

            float increment = (1 / (float)toSearch.Count) / (float)mDict.Count;
            foreach (KeyValuePair<UInt64, FileNameAndParentFrn> entry in mDict)
            {
                progress += increment;

                FileNameAndParentFrn file = (FileNameAndParentFrn)entry.Value;

                string name = file.Name;
                string lower = name.ToLower();

                GameInfo game;
                if (GameManager.Instance.GameInfos.TryGetValue(lower, out game))
                {
                    string path = mft.GetFullPath(file);
                    if (path.Contains("$Recycle.Bin"))
                    {
                        // noope
                        continue;
                    }

                    // check if the Context matches
                    string[] context = game.ExecutableContext.Split(';');
                    string dir = Path.GetDirectoryName(path);
                    bool notAdd = false;
                    for (int j = 0; j < context.Length; j++)
                    {
                        string con = Path.Combine(dir, context[j]);
                        if (!File.Exists(con) &&
                            !Directory.Exists(con))
                        {
                            notAdd = true;
                            break;
                        }
                    }

                    if (notAdd)
                    {
                        continue;
                    }

                    Invoke(new Action(delegate
                    {
                        listGames.Items.Add(game.GameName + " - " + path);
                        listGames.Invalidate();
                    }));

                    LogManager.Log("Found game: {0}, full path: {1}", game.GameName, path);
                    UserGameInfo uinfo = new UserGameInfo();
                    uinfo.InitializeDefault(game, path);
                    GameManager.Instance.User.Games.Add(uinfo);
                    GameManager.Instance.SaveUserProfile();
                }
            }

            UpdateProgress();

        }
    }
}