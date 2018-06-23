using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming.Coop.Api
{
    [Serializable]
    public class IgdbGameCover
    {
        public string url { get; set; }
        public string cloudinary_id { get; set; }
        public string width { get; set; }
        public string height { get; set; }
    }
}
