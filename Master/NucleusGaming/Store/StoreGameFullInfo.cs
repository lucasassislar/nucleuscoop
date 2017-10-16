using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Store
{
    /// <summary>
    /// Full description info of a game
    /// </summary>
    public class StoreGameFullInfo
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

        public string Description { get; set; }
        public string[] Screenshots { get; set; }
        public int Version { get; set; }
        public int PlatformVersion { get; set; }
    }
}
