using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.PackageManager
{
    /// <summary>
    /// Information about a game that is shown in the repository.
    /// This class should contain ONLY essential information, as it is cached
    /// in the first run of the app
    /// </summary>
    public class RepositoryGameInfo
    {
        /// <summary>
        /// ID must be unique to the server
        /// </summary>
        public string ID { get; set; }

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
        public string Developer { get; set; }
    }
}