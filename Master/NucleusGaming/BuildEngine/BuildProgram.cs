using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Build
{
    public class BuildProgram
    {
        private DirectoryInfo startLocation;
        private FileInfo solutionFile;
        private DirectoryInfo solutionRoot;
        public BuildProgram()
        {
            startLocation = new DirectoryInfo(AssemblyUtil.GetStartFolder());
        }

        public FileInfo GetSolutionFile()
        {
            if (solutionFile == null)
            {
                solutionFile = RecursiveGetSolutionFile(startLocation);
            }
            return solutionFile;
        }

        public DirectoryInfo GetStartLocation()
        {
            return startLocation;
        }

        public DirectoryInfo GetSolutionRoot()
        {
            if (solutionRoot == null)
            {
                GetSolutionFile();
                if (solutionFile != null)
                {
                    solutionRoot = new DirectoryInfo(Path.GetDirectoryName(solutionFile.FullName));
                }
            }
            return solutionRoot;
        }

        private FileInfo RecursiveGetSolutionFile(DirectoryInfo dir)
        {
            FileInfo[] files = dir.GetFiles("*.sln");
            if (files.Length > 0)
            {
                return files.First();
            }

            if (dir.Parent == dir.Parent.Root)
            {
                return null;
            }
            return RecursiveGetSolutionFile(dir.Parent);
        }
    }
}
