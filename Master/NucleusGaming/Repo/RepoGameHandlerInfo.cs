using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Repo
{
    /// <summary>
    /// Information about a game that is shown in the repository.
    /// This class should contain ONLY essential information, as it is cached
    /// in the first run of the app - its essentially 
    /// a stripped-down version of the RepoGameHandlerFullInfo
    /// </summary>
    public class RepoGameHandlerInfo
    {
        /// <summary>
        /// ID must be unique to the server
        /// </summary>
        public string HandlerID { get; set; }

        /// <summary>
        /// ID of the game - should be unique to the game (usually we use the Steam Game ID here)
        /// </summary>
        public string GameID { get; set; }

        /// <summary>
        /// The title of the game's package
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The name of the executable
        /// </summary>
        public string ExeName { get; set; }

        /// <summary>
        /// The name of the developer that uploaded this game handler
        /// </summary>
        public string Dev { get; set; }

        public string MD5 { get; set; }

        public int V { get; set; }
    }
}