using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    public class UserGameInfo
    {
        private GameInfo game;
        private List<GameProfile> profiles;
        private string exePath;

        [JsonIgnore]
        public GameInfo Game
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
                game = GameManager.Instance.Games[value];
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

        public void InitializeDefault(GameInfo game, string exePath)
        {
            this.game = game;
            this.exePath = exePath;
            this.profiles = new List<GameProfile>();
        }
    }
}
