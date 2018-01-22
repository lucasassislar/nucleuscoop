using Nucleus.Gaming.Coop.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Coop
{
    public abstract class HandlerModule
    {
        public abstract int Order { get;  }
        //protected IList<PlayerInfo> players;

        //private UserGameInfo userGame;
        //private GameProfile profile;
        //private GenericHandlerData handlerData;

        public abstract bool Initialize(GameHandler handler, HandlerData handlerData, UserGameInfo game, GameProfile profile);
        //{
        //    this.userGame = game;
        //    this.profile = profile;
        //    this.handlerData = handlerData;

        //    return true;
        //}

        //public abstract void Initialize(IList<PlayerInfo> players);
        //{
        //    this.players = players;
        //}

        public abstract void PrePlay();

        public abstract void PrePlayPlayer(PlayerInfo playerInfo, int index);
        public abstract void PlayPlayer(PlayerInfo playerInfo, int index, HandlerContext context);

        public abstract void Tick(double delayMs);
    }
}
