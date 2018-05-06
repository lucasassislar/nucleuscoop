using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    public class SaveInfo : Dictionary<string, string>
    {
        public SaveInfo(string section, string key, string value)
        {
            Add("Section", section);
            Add("Key", key);
            Add("Value", value);
        }

        public SaveInfo()
        {

        }
    }
}
