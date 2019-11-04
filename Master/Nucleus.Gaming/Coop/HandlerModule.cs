using Nucleus.Gaming.Coop.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Coop {
    /// <summary>
    /// Represents all a Handler Module needs to implement to be able to be used by the engine
    /// </summary>
    public abstract class HandlerModule {
        public abstract int Order { get; }

        public abstract bool Initialize(GameHandler handler, HandlerData handlerData, UserGameInfo game, GameProfile profile);

        public PlayerInfo Player { get; private set; }

        public HandlerModule(PlayerInfo parentPlayer) {
            Player = parentPlayer;
        }

        public abstract void PrePlayPlayer(int index, HandlerContext context);
        public abstract void PlayPlayer(int index, HandlerContext context);

        public abstract void Tick(double delayMs);
    }
}
