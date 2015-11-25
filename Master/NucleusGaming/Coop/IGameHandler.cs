using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nucleus.Gaming
{
    /// <summary>
    /// Implements a game's splitscreen handler
    /// </summary>
    public interface IGameHandler
    {
        /// <summary>
        /// The update interval to call Update(). Set to 0 or -1
        /// to disable 
        /// </summary>
        int TimerInterval { get; }

        ///bool Initialize(string gameFilename, List<PlayerInfo> players, Dictionary<string, GameOption> options, List<Control> addSteps, int titleHeight);

        bool Initialize(UserGameInfo game, GameProfile profile);


        string Play();
        void Update(int delayMS);

        void End();

        bool HasEnded { get; }
        event Action Ended;
    }
}
