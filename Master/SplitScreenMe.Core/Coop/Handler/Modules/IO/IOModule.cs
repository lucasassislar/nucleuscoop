using SplitScreenMe.Core.Handler;
using Nucleus.Gaming.Tools.GameStarter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Nucleus.Gaming;

namespace SplitScreenMe.Core.Modules {
    public class IOModule : HandlerModule {
        private UserGameInfo userGame;
        private GameProfile profile;
        private HandlerData handlerData;

        public override int Order { get { return 10; } }

        public override bool Initialize(GameHandler handler, HandlerData handlerData, UserGameInfo game, GameProfile profile) {
            this.userGame = game;
            this.profile = profile;
            this.handlerData = handlerData;

            handlerData.RegisterAdditional(Folder.Documents.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            handlerData.RegisterAdditional(Folder.MainGameFolder.ToString(), Path.GetDirectoryName(game.ExePath));
            handlerData.RegisterAdditional(Folder.InstancedGameFolder.ToString(), Path.GetDirectoryName(game.ExePath));

            return true;
        }

        public string NucleusRootFolder { get; private set; }

        /// <summary>
        /// Path to the current game instance (to be changed)
        /// </summary>
        public string TempDir { get; private set; }

        public string ExeFolder { get; private set; }
        public string RootFolder { get; private set; }
        public string WorkingFolder { get; private set; }

        public string LinkedExePath { get; private set; }
        public string LinkedFolder { get; private set; }
        public string LinkedWorkingDir { get; private set; }

        public SymlinkGameData SymlinkData { get; private set; }

        public IOModule(PlayerInfo player)
            : base(player) {
        }

        public override void PrePlayPlayer(int index, HandlerContext context) {
            NucleusRootFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            TempDir = GameManager.GetTempFolder(handlerData);

            ExeFolder = Path.GetDirectoryName(userGame.ExePath);
            RootFolder = ExeFolder;
            WorkingFolder = ExeFolder;

            if (!string.IsNullOrEmpty(handlerData.ExecutablePath)) {
                RootFolder = StringUtil.ReplaceCaseInsensitive(ExeFolder, handlerData.ExecutablePath.ToLower(), "");
            }
            if (!string.IsNullOrEmpty(handlerData.WorkingFolder)) {
                RootFolder = StringUtil.ReplaceCaseInsensitive(ExeFolder, handlerData.WorkingFolder.ToLower(), "");
                WorkingFolder = Path.Combine(ExeFolder, handlerData.WorkingFolder.ToLower());
            }

            if (handlerData.SymlinkGame || handlerData.HardcopyGame) {
                List<string> dirExclusions = new List<string>();
                List<string> fileExclusions = new List<string>();
                List<string> fileCopies = new List<string>();

                // symlink the game folder (and not the bin folder, if we have one)
                LinkedFolder = Path.Combine(TempDir, "Instance" + index);

                try {
                    if (Directory.Exists(LinkedFolder)) {
                        Directory.Delete(LinkedFolder, true);
                    }
                } catch { }

                Directory.CreateDirectory(LinkedFolder);

                LinkedWorkingDir = LinkedFolder;

                if (string.IsNullOrEmpty(handlerData.ExecutablePath)) {
                    LinkedExePath = Path.Combine(LinkedWorkingDir, Path.GetFileName(this.userGame.ExePath));
                    if (!string.IsNullOrEmpty(handlerData.WorkingFolder)) {
                        LinkedWorkingDir = Path.Combine(LinkedFolder, handlerData.WorkingFolder);
                        dirExclusions.Add(handlerData.WorkingFolder);
                    }
                } else {
                    dirExclusions.Add(handlerData.ExecutablePath);
                    LinkedExePath = Path.Combine(LinkedWorkingDir, handlerData.ExecutablePath, Path.GetFileName(this.userGame.ExePath));

                    if (!string.IsNullOrEmpty(handlerData.WorkingFolder)) {
                        LinkedWorkingDir = Path.Combine(LinkedFolder, handlerData.WorkingFolder);
                        dirExclusions.Add(handlerData.WorkingFolder);
                    } else {
                        LinkedWorkingDir = Path.Combine(LinkedFolder, handlerData.ExecutablePath);
                    }
                }

                // some games have save files inside their game folder, so we need to access them inside the loop
                handlerData.RegisterAdditional(Folder.InstancedGameFolder.ToString(), LinkedFolder);

                if (handlerData.Hook.CustomDllEnabled) {
                    fileExclusions.Add("xinput1_3.dll");
                    fileExclusions.Add("ncoop.ini");
                }
                if (!handlerData.SymlinkExe) {
                    fileCopies.Add(handlerData.ExecutableName.ToLower());
                }

                // additional ignored files by the generic info
                if (handlerData.FileSymlinkExclusions != null) {
                    string[] symlinkExclusions = handlerData.FileSymlinkExclusions;
                    for (int k = 0; k < symlinkExclusions.Length; k++) {
                        string s = symlinkExclusions[k];
                        // make sure it's lower case
                        fileExclusions.Add(s.ToLower());
                    }
                }
                if (handlerData.FileSymlinkCopyInstead != null) {
                    string[] fileSymlinkCopyInstead = handlerData.FileSymlinkCopyInstead;
                    for (int k = 0; k < fileSymlinkCopyInstead.Length; k++) {
                        string s = fileSymlinkCopyInstead[k];
                        // make sure it's lower case
                        fileCopies.Add(s.ToLower());
                    }
                }
                if (handlerData.DirSymlinkExclusions != null) {
                    string[] symlinkExclusions = handlerData.DirSymlinkExclusions;
                    for (int k = 0; k < symlinkExclusions.Length; k++) {
                        string s = symlinkExclusions[k];
                        // make sure it's lower case
                        dirExclusions.Add(s.ToLower());
                    }
                }

                string[] fileExclusionsArr = fileExclusions.ToArray();
                string[] fileCopiesArr = fileCopies.ToArray();

                if (handlerData.HardcopyGame) {
                    // copy the directory
                    int exitCode;
                    FileUtil.CopyDirectory(RootFolder, new DirectoryInfo(RootFolder), LinkedFolder, out exitCode, dirExclusions.ToArray(), fileExclusionsArr, true);
                } else {
                    SymlinkGameData symData = new SymlinkGameData();
                    symData.SourcePath = RootFolder;
                    symData.DestinationPath = LinkedFolder;
                    symData.DirExclusions = dirExclusions.ToArray();
                    symData.FileExclusions = fileExclusionsArr;
                    symData.FileCopies = fileCopiesArr;
                    SymlinkData = symData;

                    //int exitCode;
                    //WinDirectoryUtil.LinkDirectory(RootFolder, new DirectoryInfo(RootFolder), LinkedFolder, out exitCode, dirExclusions.ToArray(), fileExclusionsArr, fileCopiesArr, true);
                }
            } else {
                LinkedExePath = userGame.ExePath;
                LinkedWorkingDir = RootFolder;
                LinkedFolder = WorkingFolder;
            }

            context.InstancedExePath = LinkedExePath;
            context.InstallFolder = ExeFolder;
            context.InstanceFolder = LinkedFolder;
            context.InstancedWorkingPath = LinkedWorkingDir;
        }

        public static bool IsNeeded(HandlerData data) {
            return true;
        }

        public override void PlayPlayer(int index, HandlerContext context) {

        }

        public override void Tick(double delayMs) {
        }
    }
}
