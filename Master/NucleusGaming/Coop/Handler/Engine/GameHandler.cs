using Nucleus.Gaming.Coop.Handler.Cursor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Nucleus.Gaming.Coop.Handler
{
    public class GameHandler
    {
        private UserGameInfo _userGame;
        private GameProfile _profile;
        private HandlerData _handlerData;

        private List<HandlerModule> modules;

        public T GetModule<T>()
        {
            for (int i = 0; i < modules.Count; i++)
            {
                object module = modules[i];
                if (module is T)
                {
                    return (T)module;
                }
            }
            return default(T);
        }

        public bool Initialize(HandlerData handlerData, UserGameInfo userGameInfo, GameProfile profile)
        {
            this._handlerData = handlerData;
            this._userGame = userGameInfo;
            this._profile = profile;

            modules = new List<HandlerModule>();
            foreach (ModuleInfo info in GameManager.Instance.ModuleManager.Modules)
            {
                if (info.IsNeeded(handlerData))
                {
                    modules.Add((HandlerModule)Activator.CreateInstance(info.ModuleType));
                }
            }

            // order modules
            modules = modules.OrderBy(c => c.Order).ToList();

            for (int i = 0; i < modules.Count; i++)
            {
                modules[i].Initialize(this, handlerData, userGameInfo, profile);
            }

            return true;
        }

        public RequestResult<string> Play()
        {
            List<PlayerInfo> players = _profile.PlayerData;
            for (int i = 0; i < players.Count; i++)
            {
                players[i].PlayerID = i;
            }

            var result = new RequestResult<string>();

            for (int i = 0; i < modules.Count; i++)
            {
                modules[i].PrePlay();
            }

            for (int i = 0; i < players.Count; i++)
            {
                PlayerInfo player = players[i];

                for (int j = 0; j < modules.Count; j++)
                {
                    modules[j].PrePlayPlayer(player, i);
                }

                HandlerContext context = _handlerData.CreateContext(_profile, player);
                context.PlayerID = player.PlayerID;

                _handlerData.PrePlay(context, player);

                for (int j = 0; j < modules.Count; j++)
                {
                    modules[j].PlayPlayer(player, i, context);
                }

                Thread.Sleep(TimeSpan.FromSeconds(_handlerData.PauseBetweenStarts));
            }

            return result;
        }

        public void Tick(double delayMs)
        {
            List<PlayerInfo> players = _profile.PlayerData;
            for (int i = 0; i < players.Count; i++)
            {
                PlayerInfo player = players[i];

            }
            for (int j = 0; j < modules.Count; j++)
            {
                modules[j].Tick(delayMs);
            }
        }

        public void End()
        {

        }
    }
}
