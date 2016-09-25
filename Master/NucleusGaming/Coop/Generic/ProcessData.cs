using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using WindowScrape.Types;

namespace Nucleus.Gaming
{
    public class ProcessData
    {
        private Process process;

        public Point Position;
        public HwndObject HWND;
        public bool HWNDRetry;

        public Size Size;
        public bool Setted;
        public bool SettedKeyboard;

        public bool Register0;

        public bool KilledMutexes;
        public long RegLong;

        /// <summary>
        /// A reference to the game's process, if it's running
        /// </summary>
        public Process Process
        {
            get { return process; }
        }

        public ProcessData(Process proc)
        {
            this.process = proc;
        }

        public void AssignProcess(Process proc)
        {
            this.process = proc;
        }
    }
}
