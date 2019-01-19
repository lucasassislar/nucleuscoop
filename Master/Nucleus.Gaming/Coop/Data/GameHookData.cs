using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Coop
{
    /// <summary>
    /// Holds information about our custom x360ce/xinput hook dll
    /// </summary>
    public class GameHookData
    {
        public bool ClipMouse { get; set; }

        /// <summary>
        /// If the game should be run using our custom version of x360ce for gamepad control.
        /// Enabled by default as the majority of our games need it
        /// </summary>
        public bool CustomDllEnabled { get; set; } = true;

        /// <summary>
        /// If the game supports direct input joysticks
        /// </summary>
        public bool DInputEnabled { get; set; }

        /// <summary>
        /// If we should completely remove support for DirectInput input from the game
        /// </summary>
        public bool DInputForceDisable { get; set; }

        /// <summary>
        /// If the game supports xinput joysticks
        /// </summary>
        public bool XInputEnabled { get; set; }

        public string[] XInputNames { get; set; } = new string[]
        {
            "xinput1_3.dll"
        };
        public string[] XInputCopies { get; set; } = new string[]
        {
            ""
        };

        /// <summary>
        /// If xinput is enabled, if rerouting should be enabled (basically is we'll reroute directinput back to xinput,
        /// so we can track more than 4 gamepads on xinput at once)
        /// </summary>
        public bool XInputReroute { get; set; } = false;

        /// <summary>
        /// If our custom dll should set the window size and position locally, instead of the handler
        /// (inconsistent with some window sizes, look at Borderlands2.js for an example usage)
        /// </summary>
        //[Dynamic]
        //public bool SetWindowSize { get; set; } = true;

        /// <summary>
        /// If our custom dll should set the window size and position locally, instead of the handler
        /// (inconsistent with some window sizes, look at Borderlands2.js for an example usage)
        /// </summary>
        //[Dynamic]
        //public bool SetWindowPosition { get; set; }

        /// <summary>
        /// If our custom DLL should hook into the game's window and fake Window's events
        /// so we never leave focus. Depends on the ForceFocusWindowRegex variable
        /// (used for games that don't work when out of focus. See Borderlands.js)
        /// </summary>
        [Dynamic]
        public bool ForceFocus { get; set; }

        /// <summary>
        /// If force focus is enabled, this is the window we are attaching ourselves to
        /// and the window we are going to keep on top.
        /// This is used in a very specific case even out
        /// </summary>
        [Dynamic]
        public string ForceFocusWindowRegex { get; set; } = "";

        /// <summary>
        /// If the XInput hook dll should remove the height of the title bar
        /// when scaling the application
        /// </summary>
        //public bool RemoveTitleBar { get; set; }
    }
}
