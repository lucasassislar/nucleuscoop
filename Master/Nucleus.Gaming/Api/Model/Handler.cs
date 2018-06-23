using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming.Coop.Api
{
    [Serializable]
    public class Handler
    {
        public int id { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string name { get; set; }
        public string details { get; set; }
        public int currentVersion { get; set; }
        public int owner { get; set; }
        public int game { get; set; }

        //public Package CreatePackage(string packageFullPath, string packageInfos)
        //{
        //    return (Package)ApiController.CreatePackage(this.id, packageFullPath, packageInfos);
        //}
    }
}
