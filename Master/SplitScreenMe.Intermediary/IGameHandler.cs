using System;
using System.Threading;

namespace SplitScreenMe {
    /// <summary>
    /// Implements a game's splitscreen handler
    /// </summary>
    public interface IGameHandler {
        /// <summary>
        /// The update interval to call Update(). Set to 0 or -1
        /// to disable 
        /// </summary>
        double TimerInterval { get; }

        bool Initialize(IUserGameInfo game, IGameProfile profile);

        string Play();

        void Update(double delayMS);

        void End();

        bool HasEnded { get; }

        event Action Ended;

        Thread FakeFocus { get; }
    }
}
