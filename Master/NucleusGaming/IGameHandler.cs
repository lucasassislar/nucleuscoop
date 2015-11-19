using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nucleus.Gaming
{
    public interface IGameHandler
    {
        bool HideTaskBar { get; }
        int TimerInterval { get; }

        bool Initialize(string gameFilename, List<PlayerInfo> players, Dictionary<string, GameOption> options, List<Control> addSteps, int titleHeight);
        string Play();
        void Update(int delayMS);

        void End();

        bool Ended { get; }
    }
}
