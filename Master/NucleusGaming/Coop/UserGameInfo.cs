using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Coop
{
    /// <summary>
    /// Info for a game installed in the end-user machine
    /// </summary>
    public class UserGameInfo
    {
        private GenericGameInfo game;
        private List<GameProfile> profiles;
        private string exePath;
        private string gameGUID;

        [JsonIgnore]
        public GenericGameInfo Game
        {
            get
            {
                if (game == null)
                {
                    GameManager.Instance.Games.TryGetValue(gameGUID, out game);
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

        public string GameGUID
        {
            get { return gameGUID; }
            set { gameGUID = value; }
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
        /// If the game is still installed in the user machine
        /// </summary>
        /// <returns></returns>
        public bool IsGamePresent()
        {
            return File.Exists(exePath);
        }

        /// <summary>
        /// Initializes the User Game Info with known information from a game,
        /// and the path to the game's executable in the end user machine
        /// </summary>
        /// <param name="game">A reference to the </param>
        /// <param name="exePath"></param>
        public void InitializeDefault(GenericGameInfo game, string exePath)
        {
            this.game = game;
            gameGUID = game.GUID;

            this.exePath = exePath;
            this.profiles = new List<GameProfile>();
        }
    }
}
