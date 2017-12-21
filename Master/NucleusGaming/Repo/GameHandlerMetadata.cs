using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Repo
{
    /// <summary>
    /// Metadata included in the package file
    /// </summary>
    public class GameHandlerMetadata : GameHandlerPackageInfo
    {
        /// <summary>
        /// The context needed to find the executable of the game
        /// </summary>
        public string[] ExeContext { get; set; }

        /// <summary>
        /// A description for the game handler
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A list of paths to screenshots inside the game package
        /// </summary>
        public string[] Screenshots { get; set; }
    }
}
