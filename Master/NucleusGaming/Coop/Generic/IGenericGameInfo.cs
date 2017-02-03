using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    public interface IGenericGameInfo : IGameInfo
    {
        SaveType SaveType { get; }
        string SavePath { get; }

        double HandlerInterval { get; }
        //Dictionary<string, string> ModifySave { get; }

        string StartArguments { get; }

        string BinariesFolder { get; }
        bool NeedsSteamEmulation { get; }

        string SteamID { get; }

        string[] KillMutex { get; }

        string LauncherExe { get; }
        string LauncherTitle { get; }
        bool SymlinkExe { get; }
    }
}
