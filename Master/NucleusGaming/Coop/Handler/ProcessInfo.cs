using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using WindowScrape.Types;

namespace Nucleus.Gaming
{
    public class ProcessInfo
    {
        private Process process;
        public bool Finished;

        public Point Position;
        private HwndObject hWnd;
        public bool HWNDRetry;

        public Size Size;
        public bool Setted;

        public bool Register0;

        public bool KilledMutexes { get; set; }
        public long RegLong;
        public int Status;

        /// <summary>
        /// A reference to the game's process, if it's running
        /// </summary>
        public Process Process
        {
            get { return process; }
        }

        public HwndObject HWnd
        {
            get { return hWnd; }
            set { hWnd = value; }
        }

        public ProcessInfo(Process proc)
        {
            this.process = proc;
        }

        public void AssignProcess(Process proc)
        {
            this.process = proc;
        }
    }
}
