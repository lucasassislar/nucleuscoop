using System;
using System.Collections.Generic;
using System.Text;

namespace SplitScreenMe {
    public interface IGameProfile {
        List<IPlayerInfo> PlayerData { get; }

        /// <summary>
        /// A reference to the screens as they were
        /// when the user made the profile
        /// (so we can compare if a screen
        /// is missing or added)
        /// </summary>
        List<IUserScreen> Screens { get; } 

        /// <summary>
        /// Options set by the user
        /// </summary>
        Dictionary<string, object> Options { get; }

        void InitializeDefault(IGameInfo game);
    }
}
