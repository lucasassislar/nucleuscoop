using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Coop
{
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

        public void InitializeDefault()
        {
            games = new List<UserGameInfo>();
        }
    }
}
