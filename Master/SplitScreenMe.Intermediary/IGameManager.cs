using System;

namespace SplitScreenMe {
    public interface IGameManager {
        void Play(IGameHandler handler);
    }
}
