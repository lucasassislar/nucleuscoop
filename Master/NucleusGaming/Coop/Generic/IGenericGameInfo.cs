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

        string StartArguments { get; }

        string BinariesFolder { get; }
        string WorkingFolder { get; }

        bool NeedsSteamEmulation { get; }

        string SteamID { get; }

        string[] KillMutex { get; }

        string LauncherExe { get; }
        string LauncherTitle { get; }

        string[] FileSymlinkExclusions { get; }
        //bool SymlinkExe { get; }
    }
}
