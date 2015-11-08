using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    public class GameOption
    {
        public string Name;
        public string Description;
        public object Value;

        public GameOption(string name, string desc, object value)
        {
            this.Name = name;
            this.Description = desc;
            this.Value = value;
        }
    }
}
