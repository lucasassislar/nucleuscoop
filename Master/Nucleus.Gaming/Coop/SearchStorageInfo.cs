using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming.Coop {
    /// <summary>
    /// 
    /// </summary>
    public struct SearchStorageInfo {
        public DriveInfo Drive { get; private set; }
        public string Info { get; private set; }

        public SearchStorageInfo(DriveInfo drive) {
            Drive = drive;
            Info = "";
        }

        public void SetInfo(string info) {
            Info = info;
        }

        public override string ToString() {
            return Info;
        }
    }
}
