using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nucleus.Testing
{
    class Program
    {
        static IGameHandler handler;
        static void Main(string[] args)
        {
            GameManager manager = new GameManager();

            IGameInfo borderlands2 = manager.Games["720CE71B-FCBF-46C8-AC9D-C4B2BF3169E3"];
            UserGameInfo borderInfo = manager.AddGame(borderlands2, @"C:\Program Files (x86)\Steam\steamapps\common\Borderlands 2\Binaries\Win32\Borderlands2.exe");

            GameProfile profile = new GameProfile();
            profile.InitializeDefault(borderlands2);

            PlayerInfo p1 = new PlayerInfo();
            p1.monitorBounds = new Rectangle(0, 0, 960, 540);
            p1.screenIndex = 0;
            profile.PlayerData.Add(p1);

            PlayerInfo p2 = new PlayerInfo();
            p2.monitorBounds = new Rectangle(0, 540, 960, 540);
            p2.screenIndex = 0;
            profile.PlayerData.Add(p2);

            borderInfo.Profiles.Add(profile);

            manager.WaitSave();

            // try to play game with custom profile
            handler = manager.MakeHandler(borderlands2);

            handler.Initialize(borderInfo, profile);

            ThreadPool.QueueUserWorkItem(Play);
            //handler.Play();

            for (; ; )
            {
                Thread.Sleep(16);
                handler.Update(16);

                if (handler.HasEnded)
                {
                    break;
                }
            }
        }

        static void Play(object state)
        {
            handler.Play();
        }
    }
}
