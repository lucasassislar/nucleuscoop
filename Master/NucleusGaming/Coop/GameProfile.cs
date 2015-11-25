using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    public class GameProfile
    {
        private List<PlayerInfo> playerData;
        private List<UserScreen> screens;
        private Dictionary<string, object> options;

        public List<PlayerInfo> PlayerData
        {
            get { return playerData; }
        }

        /// <summary>
        /// The amount of players chosen by the user (note this is not 
        /// directly related to the player data, this is just a value used for creating it)
        /// </summary>
        public int PlayerCount
        {
            get;
            set;
        }

        /// <summary>
        /// A reference to the screens as they were
        /// when the user made the profile
        /// (so we can compare if a screen
        /// is missing or added)
        /// </summary>
        public List<UserScreen> Screens
        {
            get { return screens; }
        }

        /// <summary>
        /// Options set by the user
        /// </summary>
        public Dictionary<string, object> Options
        {
            get { return options; }
        }

        public GameProfile()
        {

        }

        public void InitializeDefault(GameInfo game)
        {
            if (playerData == null)
            {
                playerData = new List<PlayerInfo>();
            }

            if (screens == null)
            {
                screens = new List<UserScreen>();
            }

            if (options == null)
            {
                options = new Dictionary<string, object>();

                foreach (var opt in game.Options)
                {
                    options.Add(opt.Key, opt.Value.Value);
                }
            }
        }
    }
}
