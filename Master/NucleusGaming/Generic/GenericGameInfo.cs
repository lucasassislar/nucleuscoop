using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    public abstract class GenericGameInfo : GameInfo
    {
        public abstract GenericGameSaveType SaveType { get; }
        public abstract string SavePath { get; }

        public abstract Dictionary<string, string> ModifySave { get; }

        public abstract string StartArguments { get; }

        public abstract string BinariesFolder { get; }
        public abstract string SymlinkFilter { get; }
    }
}
