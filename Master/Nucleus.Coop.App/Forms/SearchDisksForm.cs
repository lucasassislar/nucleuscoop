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

namespace Nucleus.Coop.App.Forms
{
    public partial class SearchDisksForm : BaseForm
    {
        public struct SearchDriveInfo
        {
            public DriveInfo Drive { get; private set; }
            public string Info { get; private set; }

            public SearchDriveInfo(DriveInfo drive)
            {
                Drive = drive;
                Info = "";
            }

            public void SetInfo(string info)
            {
                Info = info;
            }

            public override string ToString()
            {
                return Info;
            }
        }

        private float progress;
        private float lastProgress;

        private List<SearchDriveInfo> drivesToSearch;

        private bool searching;
        private int drivesFinishedSearching;
        private MainForm main;

        public SearchDisksForm(MainForm main)
        {
            this.main = main;
            InitializeComponent();

            DriveInfo[] drives = DriveInfo.GetDrives();
            CheckedListBox checkedBox = listBox_drives;

            for (int i = 0; i < drives.Length; i++)
            {
                DriveInfo drive = drives[i];

                if (drive.DriveType == DriveType.CDRom ||
                    drive.DriveType == DriveType.Network)
                {
                    // CDs cannot use NTFS
                    // and network I'm not even trying
                    continue;
                }

                SearchDriveInfo d = new SearchDriveInfo(drive);

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

                        d.SetInfo(drive.Name + " " + used + " GB used");
                        checkedBox.Items.Add(d, true);
                    }
                    catch
                    {
                        // notify user of crash
                        d.SetInfo(drive.Name + " (Not authorized)");
                        checkedBox.Items.Add(d, CheckState.Indeterminate);
                    }
                }
                else
                {
                    // user might want to get that drive ready
                    d.SetInfo(drive.Name + " (Drive not ready)");
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
            if (searching)
            {
                e.Cancel = true;
            }

            base.OnFormClosing(e);
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            if (searching)
            {
                return;
            }

            if (GameManager.Instance.User.InstalledHandlers.Count == 0)
            {
                MessageBox.Show("You have no game handlers installed. No games to search for.");
                //return;
            }

            btn_search.Enabled = false;
            searching = true;
            drivesFinishedSearching = 0;

            drivesToSearch = new List<SearchDriveInfo>();
            CheckedListBox checkedBox = listBox_drives;

            for (int i = 0; i < checkedBox.CheckedItems.Count; i++)
            {
                SearchDriveInfo info = (SearchDriveInfo)checkedBox.CheckedItems[i];
                drivesToSearch.Add(info);
            }

            SearchDrives();
        }

        private void SearchDrives()
        {
            for (int i = 0; i < drivesToSearch.Count; i++)
            {
                ThreadPool.QueueUserWorkItem(SearchDrive, i);
            }
        }

        private void UpdateProgress(float toAdd)
        {
            progress += toAdd;

            float dif = progress - lastProgress;
            // only update after >.5% or if the user has just requested an update
            if (dif > 0.005f || toAdd == 0)
            {
                lastProgress = progress;
                if (this.IsDisposed)
                {
                    return;
                }

                Invoke(new Action(delegate
                {
                    if (this.IsDisposed || progress_search.IsDisposed)
                    {
                        return;
                    }
                    progress_search.Value = Math.Min(100, (int)(progress * 100));
                }));
            }
        }

        private void SearchDrive(object state)
        {
            int driveIndex = (int)state;
            SearchDriveInfo info = drivesToSearch[driveIndex];
            if (!info.Drive.IsReady)
            {
                drivesFinishedSearching++;
                return;
            }

            float totalDiskPc = 1 / (float)drivesToSearch.Count;
            float thirdDiskPc = totalDiskPc / 3.0f;

            // 1/3 done, we started the operation
            UpdateProgress(thirdDiskPc);

            Log.WriteLine($"> Searching drive {info.Drive.Name} for game executables");

            Dictionary<ulong, FileNameAndParentFrn> allExes = new Dictionary<ulong, FileNameAndParentFrn>();
            MFTReader mft = new MFTReader();
            mft.Drive = info.Drive.RootDirectory.FullName;

            // TODO: search only for specific games?
            mft.EnumerateVolume(out allExes, new string[] { ".exe" });

            UpdateProgress(thirdDiskPc); // 2/3 done

            float perFilePCIncrement = thirdDiskPc / (float)allExes.Count;
            foreach (KeyValuePair<UInt64, FileNameAndParentFrn> entry in allExes)
            {
                UpdateProgress(perFilePCIncrement);

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
                        Log.WriteLine($"> Found new game ID {uinfo.GameID} on drive {info.Drive.Name}");
                        Invoke(new Action(delegate
                        {
                            list_games.Items.Add(GameManager.Instance.NameManager.GetGameName(uinfo.GameID) + " - " + path);
                            list_games.Invalidate();
                            main.NewUserGame(uinfo);
                        }));
                    }
                }
            }

            drivesFinishedSearching++;
            if (drivesFinishedSearching == drivesToSearch.Count)
            {
                searching = false;
                Invoke(new Action(delegate
                {
                    progress = 1;
                    UpdateProgress(0);
                    btn_search.Enabled = true;

                    main.RefreshGames();
                    MessageBox.Show("Finished searching!");
                }));
            }
        }
    }
}