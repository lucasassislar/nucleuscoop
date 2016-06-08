using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    public class GenericGameHandler : IGameHandler
    {
        protected bool hasEnded;
        protected int timerInterval = 1000;

        public virtual bool HasEnded
        {
            get { return hasEnded; }
        }

        public int TimerInterval
        {
            get { return timerInterval; }
        }

        public event Action Ended;

        public void End()
        {
            hasEnded = true;
        }

        public bool Initialize(UserGameInfo game, GameProfile profile)
        {
            // see if we have any save game to backup
            GenericGameInfo gameInfo = game.Game as GenericGameInfo;
            if (gameInfo == null)
            {
                // you fucked up
                return false;
            }

            if (gameInfo.SaveType == GenericGameSaveType.None)
            {
                return true;
            }

            string savePath = gameInfo.SavePath;

            return true;
        }

        public bool ProcessString(string str)
        {
            for (int index = 0; index != -1; index = str.IndexOf("$"))
            {
                int end = str.IndexOf("$", index + 1);
                if (end == -1)
                {
                    // !! error !!
                    return false;
                }

                string toQuery = str.Substring(index, end - index);

            }

            return true;
        }

        public string Play()
        {
            return string.Empty;
        }

        public void Update(int delayMS)
        {
        }
    }
}
