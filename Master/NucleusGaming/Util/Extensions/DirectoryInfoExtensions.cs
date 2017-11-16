using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    public static class DirectoryInfoExtensions
    {
        /// <summary>
        /// Searches for a specific file in the current directory
        /// </summary>
        /// <param name="dir">The directory to search in</param>
        /// <param name="searchPattern"></param>
        /// <param name="searchOption"></param>
        /// <returns></returns>
        public static FileInfo GetFile(this DirectoryInfo dir, string searchPattern, SearchOption searchOption)
        {
            return dir.GetFiles(searchPattern, searchOption).FirstOrDefault();
        }
    }
}
