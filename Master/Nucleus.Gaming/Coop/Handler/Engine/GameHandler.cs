using Nucleus.Gaming.Coop.Handler.Cursor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Nucleus.Gaming.Coop.Handler
{
    /// <summary>
    /// Base class that loads modules based on their need
    /// </summary>
    public class GameHandler
    {
        private UserGameInfo _userGame;
        private GameProfile _profile;
        private HandlerDataManager _handlerManager;

        private List<HandlerModule> modules;

        /// <summary>
        /// Action callback when the game session has ended
        /// </summary>
        public event Action Ended;

        /// <summary>
        /// Gets a module by its type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
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

        public bool Initialize(HandlerDataManager handlerManager, UserGameInfo userGameInfo, GameProfile profile)
        {
            this._handlerManager = handlerManager;
            this._userGame = userGameInfo;
            this._profile = profile;

            modules = new List<HandlerModule>();
            foreach (ModuleInfo info in GameManager.Instance.ModuleManager.Modules)
            {
                if (info.IsNeeded(handlerManager.HandlerData))
                {
                    modules.Add((HandlerModule)Activator.CreateInstance(info.ModuleType));
                }
            }

            // order modules
            modules = modules.OrderBy(c => c.Order).ToList();

            for (int i = 0; i < modules.Count; i++)
            {
                modules[i].Initialize(this, handlerManager.HandlerData, userGameInfo, profile);
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

                HandlerContext context = _handlerManager.HandlerData.CreateContext(_profile, player);
                context.PlayerID = player.PlayerID;

                for (int j = 0; j < modules.Count; j++)
                {
                    modules[j].PrePlayPlayer(player, i, context);
                }

                _handlerManager.Play(context, player);

                for (int j = 0; j < modules.Count; j++)
                {
                    modules[j].PlayPlayer(player, i, context);
                }

                Thread.Sleep(TimeSpan.FromSeconds(_handlerManager.HandlerData.PauseBetweenStarts));
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
            if (Ended != null)
            {
                Ended();
            }
        }
    }
}
