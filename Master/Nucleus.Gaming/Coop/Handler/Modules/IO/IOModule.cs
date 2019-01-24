using Nucleus.Gaming.Coop.Handler;
using Nucleus.Gaming.Platform.Windows.IO;
using Nucleus.Gaming.Tools.GameStarter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nucleus.Gaming.Coop.Modules {
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

        private string nucleusRootFolder;
        private string tempDir;
        private string exeFolder;
        private string rootFolder;
        private string workingFolder;

        public string NucleusRootFolder { get { return nucleusRootFolder; } }


        /// <summary>
        /// Path to the current game instance (to be changed)
        /// </summary>
        public string TempDir { get { return tempDir; } }


        public string ExeFolder { get { return exeFolder; } }
        public string RootFolder { get { return rootFolder; } }
        public string WorkingFolder { get { return workingFolder; } }

        private string linkedExePath;
        private string linkFolder;
        private string linkWorkingDir;

        public string LinkedExePath { get { return linkedExePath; } }
        public string LinkedFolder { get { return linkFolder; } }
        public string LinkedWorkingDir { get { return linkWorkingDir; } }

        public IOModule(PlayerInfo player)
            : base(player) {
        }


        public override void PrePlayPlayer( int index, HandlerContext context) {
            nucleusRootFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            tempDir = GameManager.GetTempFolder(handlerData);

            exeFolder = Path.GetDirectoryName(userGame.ExePath);
            rootFolder = exeFolder;
            workingFolder = exeFolder;

            if (!string.IsNullOrEmpty(handlerData.ExecutablePath)) {
                rootFolder = StringUtil.ReplaceCaseInsensitive(exeFolder, handlerData.ExecutablePath.ToLower(), "");
            }
            if (!string.IsNullOrEmpty(handlerData.WorkingFolder)) {
                rootFolder = StringUtil.ReplaceCaseInsensitive(exeFolder, handlerData.WorkingFolder.ToLower(), "");
                workingFolder = Path.Combine(exeFolder, handlerData.WorkingFolder.ToLower());
            }

            if (handlerData.SymlinkGame || handlerData.HardcopyGame) {
                List<string> dirExclusions = new List<string>();
                List<string> fileExclusions = new List<string>();
                List<string> fileCopies = new List<string>();

                // symlink the game folder (and not the bin folder, if we have one)
                linkFolder = Path.Combine(tempDir, "Instance" + index);

                try {
                    if (Directory.Exists(linkFolder)) {
                        Directory.Delete(linkFolder, true);
                    }
                } catch { }

                Directory.CreateDirectory(linkFolder);

                linkWorkingDir = linkFolder;

                if (string.IsNullOrEmpty(handlerData.ExecutablePath)) {
                    linkedExePath = Path.Combine(linkWorkingDir, Path.GetFileName(this.userGame.ExePath));
                    if (!string.IsNullOrEmpty(handlerData.WorkingFolder)) {
                        linkWorkingDir = Path.Combine(linkFolder, handlerData.WorkingFolder);
                        dirExclusions.Add(handlerData.WorkingFolder);
                    }
                } else {
                    dirExclusions.Add(handlerData.ExecutablePath);
                    linkedExePath = Path.Combine(linkWorkingDir, handlerData.ExecutablePath, Path.GetFileName(this.userGame.ExePath));

                    if (!string.IsNullOrEmpty(handlerData.WorkingFolder)) {
                        linkWorkingDir = Path.Combine(linkFolder, handlerData.WorkingFolder);
                        dirExclusions.Add(handlerData.WorkingFolder);
                    } else {
                        linkWorkingDir = Path.Combine(linkFolder, handlerData.ExecutablePath);
                    }
                }

                // some games have save files inside their game folder, so we need to access them inside the loop
                handlerData.RegisterAdditional(Folder.InstancedGameFolder.ToString(), linkFolder);

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
                    FileUtil.CopyDirectory(rootFolder, new DirectoryInfo(rootFolder), linkFolder, out exitCode, dirExclusions.ToArray(), fileExclusionsArr, true);
                } else {
                    //SymlinkGameData symData = new SymlinkGameData();
                    //symData.SourcePath = rootFolder;
                    //symData.DestinationPath = linkFolder;
                    //symData.DirExclusions = dirExclusions.ToArray();
                    //symData.FileExclusions = fileExclusionsArr;
                    //symData.FileCopies = fileCopiesArr;

                    int exitCode;
                    WinDirectoryUtil.LinkDirectory(rootFolder, new DirectoryInfo(rootFolder), linkFolder, out exitCode, dirExclusions.ToArray(), fileExclusionsArr, fileCopiesArr, true);
                }
            } else {
                linkedExePath = userGame.ExePath;
                linkWorkingDir = rootFolder;
                linkFolder = workingFolder;
            }

            context.InstancedExePath = linkedExePath;
            context.InstallFolder = exeFolder;
            context.InstanceFolder = linkFolder;
            context.InstancedWorkingPath = linkWorkingDir;
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
