using Newtonsoft.Json;
using Nucleus.Gaming.Coop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using WindowScrape.Types;

namespace Nucleus.Gaming
{
    [AppDomainShared]
    public class ProcessInfo
    {
        public bool Finished;

        public Point Position;
        public bool HWNDRetry;

        public Size Size;
        public bool Setted;

        public bool Register0;

        public bool KilledMutexes { get; set; }
        public long RegLong;
        public int Status;

        private Process process;
        private HwndObject hWnd;

        /// <summary>
        /// A reference to the game's process, if it's running
        /// </summary>
        [JsonIgnore]
        public Process Process
        {
            get { return process; }
        }

        public HwndObject HWnd
        {
            get { return hWnd; }
            set { hWnd = value; }
        }

        private ProcessInfo()
        {

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
