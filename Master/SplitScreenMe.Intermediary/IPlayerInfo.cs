using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SplitScreenMe {
    public interface IPlayerInfo {
        /// <summary>
        /// The bounds of this player's game screen
        /// </summary>
        Rectangle MonitorBounds { get; set; }


        // Runtime

        Rectangle SourceEditBounds { get; set; }

        /// <summary>
        /// A temporary rectangle to show the user
        /// where the game screen is going to be located
        /// </summary>
        Rectangle EditBounds { get; set; }

        /// <summary>
        /// The index of this player
        /// </summary>
        int ScreenIndex { get; set; }

        /// <summary>
        /// A custom tag object for handlers to store data in
        /// </summary>
        object Tag { get; set; }

        /// <summary>
        /// Information about the game's process, null if its not running
        /// </summary>
        IProcessData ProcessData { get; set; }
    }
}
