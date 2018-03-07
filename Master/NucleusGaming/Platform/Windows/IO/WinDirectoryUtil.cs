using Nucleus.Gaming.Platform.Windows.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Platform.Windows.IO
{
    public static class WinDirectoryUtil
    {
        public static void LinkFiles(string rootFolder, string destination, out int exitCode, string[] exclusions, string[] copyInstead)
        {
            exitCode = 1;

            FileInfo[] files = new DirectoryInfo(rootFolder).GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i];

                string lower = file.Name.ToLower();
                bool exclude = false;
                for (int j = 0; j < exclusions.Length; j++)
                {
                    string exc = exclusions[j];
                    if (lower.Contains(exc))
                    {
                        // check if the file is i
                        exclude = true;
                        break;
                    }
                }

                if (exclude)
                {
                    continue;
                }

                for (int j = 0; j < copyInstead.Length; j++)
                {
                    string copy = copyInstead[j];
                    if (lower.Contains(copy))
                    {
                        exclude = true;
                        break;
                    }
                }

                string relative = file.FullName.Replace(rootFolder + @"\", "");
                string linkPath = Path.Combine(destination, relative);
                if (exclude)
                {
                    // should copy!
                    File.Copy(file.FullName, linkPath, true);
                }
                else
                {
                    //CmdUtil.MkLinkFile(file.FullName, linkPath, out exitCode);
                    Kernel32Interop.CreateSymbolicLink(linkPath, file.FullName, SymbolicLink.File);
                }
            }
        }

        public static void LinkDirectory(string root, DirectoryInfo currentDir, string destination, out int exitCode, string[] dirExclusions, string[] fileExclusions, string[] fileCopyInstead, bool overrideSpecial = false)
        {
            exitCode = 1;

            bool special = overrideSpecial;
            for (int j = 0; j < dirExclusions.Length; j++)
            {
                string exclusion = dirExclusions[j];
                string fullPath = Path.Combine(root, exclusion).ToLower();

                if (fullPath.Contains(currentDir.FullName.ToLower()))
                {
                    // special case, one of our subfolders is excluded
                    special = true;
                    break;
                }
            }

            if (special)
            {
                // this folder has a child that cant be symlinked
                Directory.CreateDirectory(destination);
                //CmdUtil.LinkFiles(currentDir.FullName, destination, out exitCode, fileExclusions, fileCopyInstead);
                WinDirectoryUtil.LinkFiles(currentDir.FullName, destination, out exitCode, fileExclusions, fileCopyInstead);


                DirectoryInfo[] children = currentDir.GetDirectories();
                for (int i = 0; i < children.Length; i++)
                {
                    DirectoryInfo child = children[i];
                    LinkDirectory(root, child, Path.Combine(destination, child.Name), out exitCode, dirExclusions, fileExclusions, fileCopyInstead);
                }
            }
            else
            {
                // we symlink this directly
                //CmdUtil.MkLinkDirectory(currentDir.FullName, destination, out exitCode);
                Kernel32Interop.CreateSymbolicLink(destination, currentDir.FullName, SymbolicLink.Directory);
            }
        }
    }
}
