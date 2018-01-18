using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Coop
{
    public abstract class HandlerManager
    {
        //protected IList<PlayerInfo> players;

        //private UserGameInfo userGame;
        //private GameProfile profile;
        //private GenericHandlerData handlerData;

        public abstract bool Initialize(HandlerData handlerData, UserGameInfo game, GameProfile profile);
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

        public abstract bool CanPlay(PlayerInfo playerInfo, int index);
    }
}
