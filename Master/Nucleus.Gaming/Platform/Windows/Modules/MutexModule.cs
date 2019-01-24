using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.Handler;
using Nucleus.Gaming.Tools.GameStarter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Nucleus.Gaming.Platform.Windows {
    public class MutexModule : HandlerModule {
        private UserGameInfo userGame;
        private GameProfile profile;
        private HandlerData handlerData;
        private Dictionary<string, bool> killedMutexes;

        public override int Order { get { return 100; } }

        public MutexModule(PlayerInfo player)
            : base(player) {
        }

        public override bool Initialize(GameHandler handler, HandlerData handlerData, UserGameInfo game, GameProfile profile) {
            this.userGame = game;
            this.profile = profile;
            this.handlerData = handlerData;


            return true;
        }

        public override void PrePlayPlayer(int index, HandlerContext context) {
            
        }

        public static bool IsNeeded(HandlerData data) {
            return data.KillMutex?.Length > 0;
        }

        public override void PlayPlayer(int index, HandlerContext context) {
            ProcessInfo procData = Player.ProcessData;
            List<PlayerInfo> players = profile.PlayerData;

            // log all mutexes and their current state
            // This is needed as Saints Row 3 is now creating the Mutex
            // wayyy after the game is completely loaded, making the system think
            // there's no Mutex
            killedMutexes = new Dictionary<string, bool>();
            for (int j = 0; j < handlerData.KillMutex.Length; j++) {
                killedMutexes.Add(handlerData.KillMutex[j], false);
            }

            for (; ; )
            {
                // check for the existence of the mutexes
                // before invoking our StartGame app to kill them
                ProcessInfo pdata = Player.ProcessData;

                int total = 0;
                for (int j = 0; j < handlerData.KillMutex.Length; j++) {
                    string mutex = handlerData.KillMutex[j];
                    if (StartGameUtil.MutexExists(pdata.Process, mutex)) {
                        // mutex still exist, must kill
                        if (StartGameUtil.KillMutex(pdata.Process, mutex)) {
                            killedMutexes[mutex] = true;
                        }
                    } else {
                        // mutex doesnt exist. 
                        // Have we killed it or has it not been created yet?
                        if (killedMutexes[mutex]) {
                            total++;
                        }
                    }
                }

                if (total == handlerData.KillMutex.Length) {
                    break;
                }

                Thread.Sleep(250);
            }
        }

        public override void Tick(double delayMs) {
        }
    }
}
