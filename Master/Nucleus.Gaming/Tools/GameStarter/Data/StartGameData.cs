using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Tools.GameStarter {
    public class StartGameData {
        public GameStarterTask Task { get; set; }
        public string[] Parameters { get; set; }

        public string GetAsArguments() {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this)));
        }
    }
}
