using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Coop.IO
{
    public struct BackupFile
    {
        public string Source { get; set; }
        public string BackupPath { get; set; }

        public BackupFile(string source, string backup)
            : this()
        {
            Source = source;
            BackupPath = backup;
        }
    }
}
