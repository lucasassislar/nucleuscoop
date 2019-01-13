using Nucleus.Gaming.Coop.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Coop {
    public abstract class HandlerModule {
        public abstract int Order { get; }

        public abstract bool Initialize(GameHandler handler, HandlerData handlerData, UserGameInfo game, GameProfile profile);

        public abstract void PrePlay();

        public abstract void PrePlayPlayer(PlayerInfo playerInfo, int index, HandlerContext context);
        public abstract void PlayPlayer(PlayerInfo playerInfo, int index, HandlerContext context);

        public abstract void Tick(double delayMs);
    }
}
