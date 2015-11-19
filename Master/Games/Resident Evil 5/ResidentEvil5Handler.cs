using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Games
{
    public class ResidentEvil5Handler : IGameHandler
    {
        public bool HideTaskBar
        {
            get { return false; }
        }
        public int TimerInterval
        {
            get { return 33; }
        }

        protected string gameFileName;
        protected string folder;
        public bool Initialize(string gameFilename, List<PlayerInfo> players, Dictionary<string, GameOption> options, List<Control> addSteps, int titleHeight)
        {
            // Copy the SplitScreen files to the game folder
            this.gameFileName = gameFilename;

            folder = Path.GetDirectoryName(gameFileName);
            string splitscreenCFG = folder + "\\splitscreen.cfg";
            string splitscreenDLL = folder + "\\splitscreen.dll";
            string splitscreenEXE = folder + "\\splitscreen.exe";

            FileUtil.Write(Nucleus.Coop.Games.Resident_Evil_5.RE5Resources.splitscreen_cfg, splitscreenCFG);
            FileUtil.Write(Nucleus.Coop.Games.Resident_Evil_5.RE5Resources.splitscreen_dll, splitscreenDLL);
            FileUtil.Write(Nucleus.Coop.Games.Resident_Evil_5.RE5Resources.splitscreen_exe, splitscreenEXE);

            gameFileName = splitscreenEXE;

            return true;
        }

        private Process process;

        public string Play()
        {
            ProcessStartInfo procStart = new ProcessStartInfo(gameFileName);
            procStart.Verb = "runas";
            procStart.WorkingDirectory = folder;
            process= Process.Start(procStart);
            // PROCESS.END EVENT DOESNT WORK PROPERLY!


            return string.Empty;
        }

        private bool ended;
        public void Update(int delayMS)
        {
            if (process.HasExited)
            {
                ended = true;
            }
        }

        public void End()
        {
        }


        public bool Ended
        {
            get { return ended; }
        }
    }
}
