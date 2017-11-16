using Nucleus.Gaming.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Coop
{
    public class CoopConfig : JsPropertiesFile
    {
        public List<string> StoreHeaders { get; private set; } = new List<string>();

        public CoopConfig(string path)
             : base(path)
        {
        }
    }
}
