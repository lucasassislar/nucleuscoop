using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Coop.App
{
    // TODO rename
    public enum AppPage
    {
        /// <summary>
        /// Showing nothing (reference start value)
        /// </summary>
        None,

        /// <summary>
        /// No games are available in the system
        /// </summary>
        NoGamesInstalled,

        /// <summary>
        /// Management of Packages/scanning of games
        /// </summary>
        PackageManager,

        /// <summary>
        /// Showing multiples of the same game (for example, if the user has 2 different Borderlands installations)
        /// </summary>
        SelectGameFolder,

        /// <summary>
        /// Showing game handler
        /// </summary>
        GameHandler
    }
}
