using Nucleus.Gaming.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Coop
{
    public class GameNameManager
    {
        private Dictionary<string, string> gameNames;
        public Dictionary<string, string> GameNames { get { return gameNames; } }

        public GameNameManager()
        {
            gameNames = new Dictionary<string, string>();
        }

        public bool UpdateNaming(GameHandlerBaseMetadata info)
        {
            // TODO: better logic so repositories can agree on game name
            if (gameNames.ContainsKey(info.GameID))
            {
                return false;
            }
            gameNames.Add(info.GameID, info.Title);
            return true;
        }

        public bool UpdateNaming(GameHandlerMetadata info)
        {
            // TODO: better logic so repositories can agree on game name
            if (gameNames.ContainsKey(info.GameID))
            {
                return false;
            }
            gameNames.Add(info.GameID, info.Title);
            return true;
        }

        public string GetGameName(string gameId)
        {
            string gameName;
            if (gameNames.TryGetValue(gameId, out gameName))
            {
                return gameName;
            }
            return "Unknown";
        }
    }
}
