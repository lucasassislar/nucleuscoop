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

        public List<UserGameInfo> Games
        {
            get { return games; }
            set { games = value; }
        }

        public UserProfile()
        {
        }

        /// <summary>
        /// Initializes the user profile with all default options
        /// </summary>
        public void InitializeDefault()
        {
            games = new List<UserGameInfo>();
        }
    }
}
