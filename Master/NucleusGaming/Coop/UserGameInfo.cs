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
        private RepoGameHandlerFullInfo game;
        private List<GameProfile> profiles;
        private string exePath;

        public string GameID { get; set; }

        [JsonIgnore]
        public Bitmap Icon
        {
            get;
            set;
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
        public void InitializeDefault(RepoGameHandlerFullInfo game, string exePath)
        {
            this.game = game;

            this.exePath = exePath;
            this.profiles = new List<GameProfile>();
        }
    }
}
