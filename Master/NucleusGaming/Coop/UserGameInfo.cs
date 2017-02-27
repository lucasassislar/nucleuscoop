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
        private IGameInfo game;
        private List<GameProfile> profiles;
        private string exePath;

        [JsonIgnore]
        public IGameInfo Game
        {
            get { return game; }
        }

        [JsonIgnore]
        public Bitmap Icon
        {
            get;
            set;
        }

        public string GameGuid
        {
            get { return game.GUID; }
            set
            {
                GameManager.Instance.Games.TryGetValue(value, out game);
            }
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

        public void InitializeDefault(IGameInfo game, string exePath)
        {
            this.game = game;
            this.exePath = exePath;
            this.profiles = new List<GameProfile>();
        }
    }
}
