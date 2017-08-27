using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    public static class DirectoryInfoExtensions
    {
        public static FileInfo GetFile(this DirectoryInfo dir, string searchPattern, SearchOption searchOption)
        {
            return dir.GetFiles(searchPattern, searchOption).FirstOrDefault();
        }
    }
}
