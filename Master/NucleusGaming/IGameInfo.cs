using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    public abstract class GameInfo
    {
        public abstract Type[] Steps { get; }

        /// <summary>
        /// The executable name all in lower case
        /// </summary>
        public abstract string ExecutableName { get; }
        public abstract string GameName { get; }
        public abstract Type HandlerType { get; }

        public abstract int MaxPlayers { get; }
        public abstract int MaxPlayersOneMonitor { get; }

        public abstract bool SupportsKeyboard { get; }

        public abstract Dictionary<string, GameOption> Options { get; }

        public abstract string GUID { get; }

        public abstract SplitScreenType SupportedTypes { get; }
        public abstract bool NeedPositioning { get; }

        public override string ToString()
        {
            return GameName;
        }
    }
}
