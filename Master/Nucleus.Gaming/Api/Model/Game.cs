using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming.Coop.Api
{
    [Serializable]
    public class Game
    {
        public int id { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string name { get; set; }
        public int igdb_id { get; set; }
        public float rating { get; set; }
        public string summary { get; set; }
        public string cover_igdb_id { get; set; }
        public List<Handler> handlers { get; set; }

        //public Handler CreateHandler(string handlerName, string handlerDetails)
        //{
        //    return (Handler)ApiController.CreateHandler(this.id, handlerName, handlerDetails);
        //}
    }
}
