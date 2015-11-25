using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    public abstract class GameInfo
    {
        /// <summary>
        /// A reference to the types used to create the 
        /// form steps needed for the user to successfully
        /// start the game
        /// </summary>
        public abstract Type[] Steps { get; }

        /// <summary>
        /// The game's executable name in lower case
        /// </summary>
        public abstract string ExecutableName { get; }

        /// <summary>
        /// This string must be on the game's executable path for it to be considered
        /// this game
        /// </summary>
        public abstract string ExecutableContext { get; }

        /// <summary>
        /// The game's name
        /// </summary>
        public abstract string GameName { get; }


        /// <summary>
        /// The class to instantiate to handle the game's
        /// initialization
        /// </summary>
        public abstract Type HandlerType { get; }

        /// <summary>
        /// The maximum number of players this game can handle
        /// </summary>
        public abstract int MaxPlayers { get; }
        
        /// <summary>
        /// The maximum number of players this game can handle in 1 monitor
        /// </summary>
        public abstract int MaxPlayersOneMonitor { get; }

        /// <summary>
        /// If the game supports keyboard and mouse gameplay
        /// </summary>
        public abstract bool SupportsKeyboard { get; }

        /// <summary>
        /// Custom options that the user can modify
        /// </summary>
        public abstract Dictionary<string, GameOption> Options { get; }

        /// <summary>
        /// An unique GUID specific to the game
        /// </summary>
        public abstract string GUID { get; }


        /// <summary>
        /// Overrides ToString() to show the game's name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GameName;
        }
    }
}
