using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming.Coop.Api
{
    [Serializable]
    public class LoginData
    {
        public string token { get; set; }
        public User user { get; set; }
    }
}
