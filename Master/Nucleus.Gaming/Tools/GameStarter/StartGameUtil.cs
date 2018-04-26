using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nucleus.Gaming.Tools.GameStarter
{
    /// <summary>
    /// Util class for executing and reading output from the Nucleus.Coop.StartGame application
    /// </summary>
    public static class StartGameUtil
    {
        public static void KillMutex(Process p, params string[] mutex)
        {
            StartGameApp app = new StartGameApp();
            app.BeginKillMutex(p.Id, mutex);
            app.WaitForExit();
        }

        public static bool MutexExists(Process p, params string[] mutex)
        {
            StartGameApp app = new StartGameApp();
            app.BeginMutexExists(p.Id, mutex);
            app.WaitForExit();
            string output = app.GetOutputData();

            bool result;
            if (bool.TryParse(output, out result))
            {
                return result;
            }
            return false;
        }

        public static int StartGame(string pathToGame, string args, string workingDir = null)
        {
            StartGameApp app = new StartGameApp();
            app.BeginStartGame(pathToGame, args, workingDir);
            app.WaitForExit();
            return app.GetOutputAsProcessId();
        }
    }
}
