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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dirInfo"></param>
        /// <param name="rootInfo"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string internalGetRelativePath(DirectoryInfo dirInfo, DirectoryInfo rootInfo, string str)
        {
            if (dirInfo.FullName == rootInfo.FullName || dirInfo == null)
            {
                return str;
            }

            if (!string.IsNullOrWhiteSpace(Path.GetExtension(dirInfo.Name)))
            {
                str = dirInfo.Name;
            }
            else
            {
                str = dirInfo.Name + "\\" + str;
            }

            dirInfo = dirInfo.Parent;
            return internalGetRelativePath(dirInfo, rootInfo, str);
        }

        public static string GetRelativePath(string dirPath, string rootFolder)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            DirectoryInfo rootInfo = new DirectoryInfo(rootFolder);
            return internalGetRelativePath(dirInfo, rootInfo, "");
        }

        public static string GetRelativePath(this DirectoryInfo dirInfo, DirectoryInfo rootInfo)
        {
            return internalGetRelativePath(dirInfo, rootInfo, "");
        }

        public static string GetRelativePath(this DirectoryInfo dirInfo, string rootFolder)
        {
            DirectoryInfo rootInfo = new DirectoryInfo(rootFolder);
            return internalGetRelativePath(dirInfo, rootInfo, "");
        }
    }
}
