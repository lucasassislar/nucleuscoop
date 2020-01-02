using SplitScreenMe.Core.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Nucleus.Gaming;
using Nucleus;
using Nucleus.Tools.GameStarter;

namespace SplitScreenMe.Core.Handler {
    /// <summary>
    /// Base class that loads modules based on their need
    /// </summary>
    public class GameHandler {
        private UserGameInfo userGame;
        private GameProfile profile;
        private HandlerDataManager handlerManager;
        private bool hasKeyboardPlayer;

        /// <summary>
        /// Action callback when the game session has ended
        /// </summary>
        public event Action Ended;

        /// <summary>
        /// Gets a module by its type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetModule<T>(PlayerInfo p) {
            var modules = p.Modules;
            for (int i = 0; i < modules.Count; i++) {
                object module = modules[i];
                if (module is T) {
                    return (T)module;
                }
            }
            return default(T);
        }

        public bool Initialize(HandlerDataManager handlerManager, UserGameInfo userGameInfo, GameProfile profile) {
            this.handlerManager = handlerManager;
            this.userGame = userGameInfo;
            this.profile = profile;

            List<PlayerInfo> players = profile.PlayerData;

            // if there's a keyboard player, re-order play list
            hasKeyboardPlayer = players.Any(c => c.IsKeyboardPlayer);
            if (hasKeyboardPlayer) {
                if (handlerManager.HandlerData.KeyboardPlayerFirst) {
                    players.Sort((x, y) => y.IsKeyboardPlayer.CompareTo(x.IsKeyboardPlayer));
                } else {
                    players.Sort((x, y) => x.IsKeyboardPlayer.CompareTo(y.IsKeyboardPlayer));
                }
            }

            // create modules for each player
            for (int i = 0; i < players.Count; i++) {
                PlayerInfo player = players[i];
                List<HandlerModule> modules = player.Modules;
                foreach (ModuleInfo info in GameManager.Instance.ModuleManager.Modules) {
                    if (info.IsNeeded(handlerManager.HandlerData)) {
                        modules.Add((HandlerModule)Activator.CreateInstance(info.ModuleType, new object[] { player }));
                    }
                }

                // order modules
                player.Modules.Sort((x, y) => x.Order.CompareTo(y.Order));
                for (int j = 0; j < modules.Count; j++) {
                    modules[j].Initialize(this, handlerManager.HandlerData, userGameInfo, profile);
                }
            }

            return true;
        }

        public RequestResult<string> Play() {
            List<PlayerInfo> players = profile.PlayerData;
            for (int i = 0; i < players.Count; i++) {
                players[i].PlayerID = i;
            }
            RequestResult<String> result = new RequestResult<String>();

            List<HandlerContext> contexts = new List<HandlerContext>();
            for (int i = 0; i < players.Count; i++) {
                PlayerInfo player = players[i];

                HandlerContext context = handlerManager.HandlerData.CreateContext(profile, player, hasKeyboardPlayer);
                contexts.Add(context);

                context.PlayerID = player.PlayerID;

                List<HandlerModule> modules = player.Modules;
                for (int j = 0; j < modules.Count; j++) {
                    modules[j].PrePlayPlayer(i, context);
                }
            }

            // get all the SymlinkData from each players IOModule
            // and start the StartGame application to symlink them
            List<SymlinkGameData> symData = new List<SymlinkGameData>();
            for (int i = 0; i < players.Count; i++) {
                PlayerInfo player = players[i];
                IOModule mod = GetModule<IOModule>(player);
                SymlinkGameData data = mod.SymlinkData;
                symData.Add(data);
            }
            StartGameData symStartData = StartGameUtil.BuildSymlinkGameData(symData.ToArray());

            // check if we need to kill mutexes
            bool killingMutexes = false;
            if (handlerManager.HandlerData.KillMutex?.Length > 0) {
                // need to also kill mutexes
                killingMutexes = true;
                StartGameData scanKillData = StartGameUtil.BuildScanKillMutexData(Path.GetFileNameWithoutExtension(userGame.ExePath), players.Count, handlerManager.HandlerData.KillMutex);
                StartGameUtil.RunMultipleTasks(new StartGameData[] { symStartData, scanKillData }, true);

                // wait for confirmation file that the folders were symlinked
                // (RunMultipleTasks doesnt wait for process to finish)
                string dataFile = StartGameUtil.GetStartDataPath();
                for (; ; ) {
                    if (File.Exists(dataFile)) {
                        try {
                            string txt = File.ReadAllText(dataFile);
                            bool res;
                            if (bool.TryParse(txt, out res)) {
                                break;
                            }
                        } catch {
                        }
                    }
                    Thread.Sleep(250);
                }
                File.Delete(dataFile);
            } else {
                // just symlink the folders
                StartGameUtil.RunPreBuiltData(symStartData, true);
            }

            for (int i = 0; i < players.Count; i++) {
                PlayerInfo player = players[i];
                HandlerContext context = contexts[i];
                handlerManager.Play(context, player);

                List<HandlerModule> modules = player.Modules;
                for (int j = 0; j < modules.Count; j++) {
                    modules[j].PlayPlayer(i, context);
                }

                Thread.Sleep(TimeSpan.FromMilliseconds(handlerManager.HandlerData.PauseBetweenStarts));

                IGameProcessModule procModule = GetModule<IGameProcessModule>(player);
                if (handlerManager.HandlerData.WaitFullWindowLoad) {
                    for (; ; ) {
                        Thread.Sleep(100);

                        // check for window status
                        if (procModule.HasWindowSetup(player)) {
                            break;
                        }
                    }
                }

                if (killingMutexes) {
                    // wait for StartGame confirmation that mutexes were killed
                    string dataFile = StartGameUtil.GetStartDataPath();
                    for (; ; ) {
                        if (File.Exists(dataFile)) {
                            try {
                                string txt = File.ReadAllText(dataFile);
                                bool res;
                                if (bool.TryParse(txt, out res)) {
                                    break;
                                }
                            } catch {
                            }
                        }
                        Thread.Sleep(250);
                    }
                    File.Delete(dataFile);
                }
            }

            return result;
        }

        public void Tick(double delayMs) {
            List<PlayerInfo> players = profile.PlayerData;
            for (int i = 0; i < players.Count; i++) {
                PlayerInfo player = players[i];

                List<HandlerModule> modules = player.Modules;
                for (int j = 0; j < modules.Count; j++) {
                    modules[j].Tick(delayMs);
                }
            }
        }

        public void End() {
            if (Ended != null) {
                Ended();
            }
        }
    }
}
