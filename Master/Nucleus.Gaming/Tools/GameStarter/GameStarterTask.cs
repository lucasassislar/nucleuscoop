using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Tools.GameStarter {
    public enum GameStarterTask {
        // runs the game and orphans its process
        StartGame,

        // kills a mutex for a specific process id (ADMIN)
        KillMutex,
        // scans a specified process name until it's open and kill its mutexes (ADMIN)
        ScanKillMutex,

        // queries if a mutex is present (ADMIN)
        QueryMutex,

        // symlink the folders (ADMIN)
        SymlinkFolders,


        // several game starter tasks
        MultipleTasks,

        // list monitors resolutions with no DPI scaling
        ListMonitors,
    }
}
