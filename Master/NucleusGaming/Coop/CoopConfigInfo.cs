using Nucleus.Gaming.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Coop
{
    public class CoopConfigInfo : JsonPropertiesFile
    {
        /// <summary>
        /// Repository Headers URLs
        /// </summary>
        public List<string> RepoHeaders { get; private set; } = new List<string>();

        public CoopConfigInfo(string path)
             : base(path)
        {
        }
    }
}
