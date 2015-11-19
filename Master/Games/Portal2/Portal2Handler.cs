using Games.Left4Dead2;
using Nucleus.Gaming;
using Nucleus.Gaming.Controls;
using Nucleus.Gaming.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using WindowScrape.Types;
using Nucleus;
using System.Runtime.InteropServices;

namespace Games.Portal2
{
    public class Portal2Handler : IGameHandler
    {
        protected string executablePlace;
        protected List<PlayerInfo> players;
        protected Dictionary<string, GameOption> options;
        protected string videoFile;
        protected int titleHeight;
        protected string binFolder;
        protected string autoExec;
        protected string makeSplit;
        protected string pak01_000_path;
        protected string backupPak;

        public bool HideTaskBar
        {
            get { return true; }
        }

        public bool Initialize(string gameFilename, List<PlayerInfo> players, Dictionary<string, GameOption> options, List<Control> addSteps, int titleHeight)
        {
            this.executablePlace = gameFilename;
            this.players = players;
            this.options = options;
            this.titleHeight = titleHeight - 5;

            // Search for video file
            string exeFolder = Path.GetDirectoryName(executablePlace);
            binFolder = Path.Combine(exeFolder, "bin");

            return true;
        }

        protected SourceCfgFile videoCfg;
        private bool loaded;
        private string xinputPath;

        public string Play()
        {
            if (!SteamUtil.IsSteamRunning())
            {
                MessageBox.Show("Steam must be opened to play Portal 2 splitScreen. Open it then click OK.");
            }

            // minimize everything
            User32.MinimizeEverything();

            // copy the correct xinput to the bin folder
            byte[] xdata = GamesResources.Portal_xinput1_3;
            xinputPath = Path.Combine(binFolder, "xinput1_3.dll");
            using (MemoryStream stream = new MemoryStream(xdata))
            {
                // write to bin folder
                using (FileStream file = new FileStream(xinputPath, FileMode.Create))
                {
                    stream.WriteTo(file);
                }
            }

            int pid = StartGameUtil.StartGame(executablePlace, "-novid", 0);
            proc = Process.GetProcessById(pid);

            loaded = true;
            return string.Empty;
        }
        private Process proc;

        public bool Ended
        {
            get { return ended; }
        }
        private bool ended;

        public void Update(int delayMS)
        {
            if (proc.HasExited)
            {
                ended = true;
            }
        }

        public void End()
        {
            // delete XInput from the game's folder
            try
            {
                if (File.Exists(xinputPath))
                {
                    File.Delete(xinputPath);
                }
            }
            catch
            { // user might have clicked on stop, still on game
            }
        }


        public int TimerInterval
        {
            get { return 300; }
        }
    }
}
