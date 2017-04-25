using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    /// <summary>
    /// Holds information about our custom x360ce/xinput dll
    /// </summary>
    public class XInputInfo
    {
        /// <summary>
        /// If the game should be run using our custom version of x360ce for gamepad control.
        /// Enabled by default as the majority of our games need it
        /// </summary>
        public bool CustomDllEnabled = true;

        /// <summary>
        /// If the game supports direct input joysticks
        /// </summary>
        public bool DInputEnabled;

        /// <summary>
        /// If the game supports xinput joysticks
        /// </summary>
        public bool XInputEnabled;

        /// <summary>
        /// If our custom dll should set the window size and position locally, instead of the handler
        /// (inconsistent with some window sizes, look at Borderlands2.js for an example usage)
        /// </summary>
        [Dynamic]
        public bool SetWindowSize;

        /// <summary>
        /// If our custom DLL should hook into the game's window and fake Window's events
        /// so we never leave focus. Depends on the ForceFocusWindowName variable
        /// (used for games that don't work when out of focus. See Borderlands.js)
        /// </summary>
        [Dynamic]
        public bool ForceFocus;

        /// <summary>
        /// If force focus is enabled, this is the window we are attaching ourselves to
        /// and the window we are going to keep on top.
        /// This is used in a very specific case even out
        /// </summary>
        [Dynamic]
        public string ForceFocusWindowName = "";
    }
}
