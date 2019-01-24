using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using SlimDX.DirectInput;
using Nucleus.Gaming.Coop;
using Newtonsoft.Json;

namespace Nucleus.Gaming.Coop
{
    [AppDomainShared]
    public class PlayerInfo
    {
        [JsonIgnore]
        public List<HandlerModule> Modules { get; private set; } = new List<HandlerModule>();

        public UserScreen Owner;

        public int PlayerID;
        public bool SteamEmu;
        public bool GotLauncher;
        public bool GotGame;

        public bool IsKeyboardPlayer;
        public bool IsXInput;
        public bool IsDInput;
        public bool IsFake;

        public Guid GamepadProductGuid;
        public Guid GamepadGuid;
        public int GamepadId;
        public string GamepadName;
        public int GamepadMask;

        [JsonIgnore]
        public Joystick DInputJoystick;

        // Serialized

        /// <summary>
        /// The bounds of this player's game screen
        /// </summary>
        public Rectangle MonitorBounds
        {
            get { return monitorBounds; }
            set { monitorBounds = value; }
        }


        // Runtime

        public Rectangle SourceEditBounds
        {
            get { return sourceEditBounds; }
            set { sourceEditBounds = value; }
        }

        /// <summary>
        /// A temporary rectangle to show the user
        /// where the game screen is going to be located
        /// </summary>
        public Rectangle EditBounds
        {
            get { return editBounds; }
            set { editBounds = value; }
        }

        /// <summary>
        /// The index of this player
        /// </summary>
        public int ScreenIndex
        {
            get { return screenIndex; }
            set { screenIndex = value; }
        }

        /// <summary>
        /// Information about the game's process, null if its not running
        /// </summary>
        public ProcessInfo ProcessData
        {
            get { return processData; }
            set { processData = value; }
        }

        private Rectangle sourceEditBounds;
        private Rectangle editBounds;
        private Rectangle monitorBounds;
        private int screenIndex = -1;
        private object tag;

        private ProcessInfo processData;
        private bool assigned;
    }
}
