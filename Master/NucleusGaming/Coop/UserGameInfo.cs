using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    public class UserGameInfo
    {
        private GenericGameInfo game;
        private List<GameProfile> profiles;
        private string exePath;
        private string gameGuid = "";

        [JsonIgnore]
        public GenericGameInfo Game
        {
            get
            {
                if (game == null)
                {
                    GameManager.Instance.Games.TryGetValue(gameGuid, out game);
                }
                return game;
            }
        }

        [JsonIgnore]
        public Bitmap Icon
        {
            get;
            set;
        }

        public string GameGuid
        {
            get { return gameGuid; }
            set { gameGuid = value; }
        }

        public List<GameProfile> Profiles
        {
            get { return profiles; }
            set { profiles = value; }
        }

        public string ExePath
        {
            get { return exePath; }
            set { exePath = value; }
        }

        public UserGameInfo()
        {

        }

        /// <summary>
        /// If the game exists
        /// </summary>
        /// <returns></returns>
        public bool IsGamePresent()
        {
            return File.Exists(exePath);
        }

        public void InitializeDefault(GenericGameInfo game, string exePath)
        {
            this.game = game;
            gameGuid = game.GUID;

            this.exePath = exePath;
            this.profiles = new List<GameProfile>();
        }
    }
}
