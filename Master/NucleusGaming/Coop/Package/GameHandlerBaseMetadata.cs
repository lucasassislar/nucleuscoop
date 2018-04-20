using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Package
{
    /// <summary>
    /// Info about a package
    /// </summary>
    public class GameHandlerBaseMetadata
    {
        /// <summary>
        /// ID must be unique to the handler
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
        /// The last time this handler was updated
        /// </summary>
        public long LastUpdate { get; set; }

        /// <summary>
        /// The name of the developer that uploaded this game handler
        /// </summary>
        public string Dev { get; set; }

        /// <summary>
        /// MD5 checksum of the game's package
        /// </summary>
        public string MD5 { get; set; }

        /// <summary>
        /// Version of the handler included in the package
        /// </summary>
        public int V { get; set; }

        /// <summary>
        /// Version of the handler included in the package
        /// </summary>
        public int PlatV { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, v{1} by {2} for Nuke v{3}", Title, V, Dev, PlatV);
        }
    }
}