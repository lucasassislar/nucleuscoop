using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming.Coop.Api
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class User
    {
        public int id { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string email { get; set; }
        public string username { get; set; }
    }
}
