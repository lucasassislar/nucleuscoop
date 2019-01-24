using Nucleus.Gaming.Coop.Modules;
using Nucleus.Gaming.Tools.GameStarter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Nucleus.Gaming.Coop.Handler {
    /// <summary>
    /// Base class that loads modules based on their need
    /// </summary>
    public class GameHandler {
        private UserGameInfo userGame;
        private GameProfile profile;
        private HandlerDataManager handlerManager;

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

        private bool hasKeyboardPlayer;

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
            StartGameUtil.SymlinkGames(symData.ToArray());

            for (int i = 0; i < players.Count; i++) {
                PlayerInfo player = players[i];
                HandlerContext context = contexts[i];
                handlerManager.Play(context, player);

                List<HandlerModule> modules = player.Modules;
                for (int j = 0; j < modules.Count; j++) {
                    modules[j].PlayPlayer(i, context);
                }

                Thread.Sleep(TimeSpan.FromMilliseconds(handlerManager.HandlerData.PauseBetweenStarts));

                if (handlerManager.HandlerData.WaitFullWindowLoad) {
                    IGameProcessModule procModule = GetModule<IGameProcessModule>(player);
                    for (; ; ) {
                        Thread.Sleep(100);

                        // check for window status
                        if (procModule.HasWindowSetup(player)) {
                            break;
                        }
                    }
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
