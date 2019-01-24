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
        public static bool KillMutex(Process p, params string[] mutex)
        {
            StartGameApp app = new StartGameApp();
            app.BeginKillMutex(p.Id, mutex);
            app.WaitForExit();

            string output = app.GetOutputData();

            bool result = bool.Parse(output);
            return result;
        }

        public static bool MutexExists(Process p, string mutex) {
            StartGameApp app = new StartGameApp();
            app.BeginMutexExists(p.Id, mutex);
            app.WaitForExit();
            string output = app.GetOutputData();

            bool result = bool.Parse(output);
            return result;
        }

        public static void SymlinkGames(SymlinkGameData[] games) {
            StartGameApp app = new StartGameApp();
            app.BeginSymlinkGames(games);
            app.WaitForExit();
        }

        public static int StartGame(string pathToGame, string args, string workingDir = null)
        {
            //string folder = Path.GetDirectoryName(pathToGame);
            //string fileIdFile = Path.Combine(folder, "app.id");
            //if (File.Exists(fileIdFile)) {
            //    File.Delete(fileIdFile);
            //}

            StartGameApp app = new StartGameApp();
            app.BeginStartGame(pathToGame, args, workingDir);
            app.WaitForExit();

            //string appId = File.ReadAllText(fileIdFile);
            //return int.Parse(appId);

            return app.GetOutputAsProcessId();
        }
    }
}
