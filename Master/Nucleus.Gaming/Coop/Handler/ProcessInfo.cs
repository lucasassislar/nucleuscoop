using Newtonsoft.Json;
using Nucleus.Gaming.Coop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using WindowScrape.Types;

namespace Nucleus.Gaming {
    [AppDomainShared]
    public class ProcessInfo {
        /// <summary>
        /// A reference to the game's process, if it's running
        /// </summary>
        [JsonIgnore]
        public Process Process { get; private set; }

        /// <summary>
        /// If the play session has ended
        /// </summary>
        public bool Finished { get; set; }

        /// <summary>
        /// Position of the game window
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// Size of the game window
        /// </summary>
        public Size Size { get; set; }

        public bool HWNDRetry { get; set; }

        public bool Setted { get; set; }

        public bool Register0 { get; set; }

        public bool KilledMutexes { get; set; }

        public long RegLong { get; set; }

        public int Status { get; set; }

        public HwndObject HWnd { get; set; }

        private ProcessInfo() {

        }

        public ProcessInfo(Process proc) {
            this.Process = proc;
        }

        public void AssignProcess(Process proc) {
            this.Process = proc;
        }
    }
}
