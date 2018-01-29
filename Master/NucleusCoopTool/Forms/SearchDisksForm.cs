using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Diagnostics;
using Nucleus.Gaming.Platform.Windows.IO.MFT;
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

        private float progress;
        private float lastProgress;

        private List<SearchDriveInfo> toSearch;

        private bool searching;
        private int done;
        private bool closed;
        private MainForm main;

        public SearchDisksForm(MainForm main)
        {
            this.main = main;
            InitializeComponent();

            DriveInfo[] drives = DriveInfo.GetDrives();
            CheckedListBox checkedBox = disksBox;

            for (int i = 0; i < drives.Length; i++)
            {
                DriveInfo drive = drives[i];

                if (drive.DriveType == DriveType.CDRom)
                {
                    // CDs cannot use ntfs
                    continue;
                }

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

        protected override Size DefaultSize
        {
            get
            {
                return new Size(720, 500);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            closed = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (searching)
            {
                return;
            }

            if (GameManager.Instance.User.InstalledHandlers.Count == 0)
            {
                MessageBox.Show("You have no game handlers installed.");
                return;
            }

            btnSearch.Enabled = false;
            searching = true;
            done = 0;

            toSearch = new List<SearchDriveInfo>();
            CheckedListBox checkedBox = disksBox;

            for (int i = 0; i < checkedBox.CheckedItems.Count; i++)
            {
                SearchDriveInfo info = (SearchDriveInfo)checkedBox.CheckedItems[i];
                toSearch.Add(info);
            }

            SearchDrives();
        }

        private void SearchDrives()
        {
            for (int i = 0; i < toSearch.Count; i++)
            {
                ThreadPool.QueueUserWorkItem(SearchDrive, i);
            }
        }

        private void UpdateProgress(float toAdd)
        {
            progress += toAdd;

            float dif = progress - lastProgress;
            if (dif > 0.005f || toAdd == 0) // only update after .5% or if the user has just requested an update
            {
                lastProgress = progress;
                if (this.IsDisposed || closing)
                {
                    return;
                }

                Invoke(new Action(delegate
                {
                    if (this.IsDisposed || progressBar1.IsDisposed || closing)
                    {
                        return;
                    }
                    progressBar1.Value = Math.Min(100, (int)(progress * 100));
                }));
            }
        }

        private void SearchDrive(object state)
        {
            int i = (int)state;
            SearchDriveInfo info = toSearch[i];
            if (!info.drive.IsReady)
            {
                done++;
                return;
            }

            float totalDiskPc = 1 / (float)toSearch.Count;
            float thirdDiskPc = totalDiskPc / 3.0f;

            // 1/3 done, we started the operation
            UpdateProgress(thirdDiskPc);

            Log.WriteLine($"> Searching drive {info.drive.Name} for game executables");

            Dictionary<ulong, FileNameAndParentFrn> mDict = new Dictionary<ulong, FileNameAndParentFrn>();
            MFTReader mft = new MFTReader();
            mft.Drive = info.drive.RootDirectory.FullName;

            mft.EnumerateVolume(out mDict, new string[] { ".exe" });

            progress += thirdDiskPc; // 2/3 done
            UpdateProgress(thirdDiskPc);

            float increment = thirdDiskPc / (float)mDict.Count;
            foreach (KeyValuePair<UInt64, FileNameAndParentFrn> entry in mDict)
            {
                if (closed)
                {
                    return;
                }

                UpdateProgress(increment);

                FileNameAndParentFrn file = (FileNameAndParentFrn)entry.Value;

                string name = file.Name;
                string lower = name.ToLower();

                if (GameManager.Instance.AnyGame(lower))
                {
                    string path = mft.GetFullPath(file);
                    if (path.Contains("$Recycle.Bin") ||
                        path.Contains(@"\Instance"))
                    {
                        // noope
                        continue;
                    }

                    UserGameInfo uinfo = GameManager.Instance.TryAddGame(path);

                    if (uinfo != null)
                    {
                        Log.WriteLine($"> Found new game ID {uinfo.GameID} on drive {info.drive.Name}");
                        Invoke(new Action(delegate
                        {
                            listGames.Items.Add(GameManager.Instance.NameManager.GetGameName(uinfo.GameID) + " - " + path);
                            listGames.Invalidate();
                            main.NewUserGame(uinfo);
                        }));
                    }
                }
            }

            if (closed)
            {
                return;
            }

            done++;
            if (done == toSearch.Count)
            {
                searching = false;
                Invoke(new Action(delegate
                {
                    progress = 1;
                    UpdateProgress(0);
                    btnSearch.Enabled = true;

                    main.RefreshGames();
                    MessageBox.Show("Finished searching!");
                }));
            }
        }
        private bool closing;
        private void SearchDisksForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            closing = true;
        }
    }
}