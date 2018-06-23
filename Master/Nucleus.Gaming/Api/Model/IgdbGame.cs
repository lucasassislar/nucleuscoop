using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming.Coop.Api
{
    [Serializable]
    public class IgdbGame
    {
        public int id { get; set; }
        public string name { get; set; }
        public string summary { get; set; }
        public string rating { get; set; }
        public string category { get; set; }
        public IgdbGameReleaseDate[] release_dates { get; set; }
        public IgdbGameCover cover { get; set; }
    }
}
