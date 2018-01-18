using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Tools.GameStarter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Nucleus.Gaming.Platform.Windows
{
    public class MutexManager : HandlerManager
    {
        private UserGameInfo userGame;
        private GameProfile profile;
        private HandlerData handlerData;

        public override bool Initialize(HandlerData handlerData, UserGameInfo game, GameProfile profile)
        {
            this.userGame = game;
            this.profile = profile;
            this.handlerData = handlerData;

            return true;
        }

        public override bool CanPlay(PlayerInfo player, int index)
        {
            ProcessInfo procData = player.ProcessData;
            bool hasSetted = procData != null && procData.Setted;
            List<PlayerInfo> players = profile.PlayerData;

            if (index > 0 && (handlerData.KillMutex?.Length > 0 || !hasSetted))
            {
                PlayerInfo before = players[index - 1];
                for (; ;)
                {
                    Thread.Sleep(1000);

                    if (handlerData.KillMutex != null)
                    {
                        if (handlerData.KillMutex.Length > 0 && !before.ProcessData.KilledMutexes)
                        {
                            // check for the existence of the mutexes
                            // before invoking our StartGame app to kill them
                            ProcessInfo pdata = before.ProcessData;

                            if (StartGameUtil.MutexExists(pdata.Process, handlerData.KillMutex))
                            {
                                // mutexes still exist, must kill
                                StartGameUtil.KillMutex(pdata.Process, handlerData.KillMutex);
                                //pdata.KilledMutexes = true;
                                break;
                            }
                            else
                            {
                                // mutexes dont exist anymore
                                break;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            for (int i = 0; i < players.Count; i++)
            {

            }
            return false;
        }


    }
}
