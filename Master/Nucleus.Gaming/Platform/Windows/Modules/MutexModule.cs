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
            return false;
            return data.KillMutex?.Length > 0;
        }

        public override void PlayPlayer(int index, HandlerContext context) {
            ProcessInfo procData = Player.ProcessData;
            List<PlayerInfo> players = profile.PlayerData;

            ProcessInfo pdata = Player.ProcessData;
            for (int j = 0; j < handlerData.KillMutex.Length; j++) {
                string mutex = handlerData.KillMutex[j];
                StartGameUtil.KillMutex(pdata.Process, mutex);
            }
        }

        public override void Tick(double delayMs) {
        }
    }
}
