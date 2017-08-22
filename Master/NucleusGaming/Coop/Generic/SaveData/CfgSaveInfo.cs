using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.SaveData
{
    public class CfgSaveInfo : SaveInfo
    {
        public string Section;
        public string Key;
        public string Value;

        public CfgSaveInfo(string section, string key, string value)
        {
            this.Section = section;
            this.Key = key;
            this.Value = value;
        }
    }
}
