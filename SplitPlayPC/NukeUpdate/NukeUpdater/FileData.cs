using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Update
{
    public struct FileData
    {
        public string hash;
        public string relativePath;
        public string latestVersion;
        public bool deleted;
    }
}
