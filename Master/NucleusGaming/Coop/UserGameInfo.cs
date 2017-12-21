using Newtonsoft.Json;
using Nucleus.Gaming.Repo;
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
        [JsonIgnore]
        public Bitmap Icon
        {
            get;
            set;
        }

        public string GameID { get; set; }
        //public List<GameProfile> Profiles { get; set; }

        public string ExePath { get; set; }

        public UserGameInfo()
        {

        }

        /// <summary>
        /// If the game is still installed in the user machine
        /// </summary>
        /// <returns></returns>
        public bool IsGamePresent()
        {
            return File.Exists(ExePath);
        }

        /// <summary>
        /// Initializes the User Game Info with known information from a game,
        /// and the path to the game's executable in the end user machine
        /// </summary>
        /// <param name="game">A reference to the </param>
        /// <param name="exePath"></param>
        public void InitializeDefault(GameHandlerMetadata game, string exePath)
        {
            ExePath = exePath;
            //Profiles = new List<GameProfile>();

            GameID = game.GameID;
        }
    }
}
