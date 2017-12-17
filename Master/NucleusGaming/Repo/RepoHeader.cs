using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Repo
{
    public class RepoHeader
    {
        /// <summary>
        /// An array with all available games on this store
        /// </summary>
        public RepoGameHandlerInfo[] Games { get; set; }

        /// <summary>
        /// Root folder for all packages infos
        /// </summary>
        public string PackagesInfoRoot { get; set; }

        /// <summary>
        /// Root folder for all packages installers
        /// </summary>
        public string PackagesRoot { get; set; }

        public string RepositoryId { get; set; }

        public string Url { get; set; }
    }
}
