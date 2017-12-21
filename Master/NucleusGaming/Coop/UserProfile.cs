using Nucleus.Gaming.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Coop
{
    /// <summary>
    /// Represents a user of the Nucleus Coop application
    /// </summary>
    public class UserProfile
    {
        private List<UserGameInfo> games;
        private List<GameHandlerMetadata> installedHandlers;

        public List<UserGameInfo> Games
        {
            get { return games; }
            set { games = value; }
        }

        public List<GameHandlerMetadata> InstalledHandlers
        {
            get { return installedHandlers; }
            set { installedHandlers = value; }
        }

        public DateTime LatestMod { get; set; }

        public UserProfile()
        {
        }

        /// <summary>
        /// Initializes the user profile with all default options
        /// </summary>
        public void InitializeDefault()
        {
            if (games == null)
            {
                games = new List<UserGameInfo>();
            }

            if (installedHandlers == null)
            {
                installedHandlers = new List<GameHandlerMetadata>();
            }
        }
    }
}
