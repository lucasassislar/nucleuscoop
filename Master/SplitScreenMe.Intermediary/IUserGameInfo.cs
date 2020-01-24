using System;
using System.Collections.Generic;
using System.Text;

namespace SplitScreenMe {
    public interface IUserGameInfo {
        string GameGuid { get; set; }

        List<IGameProfile> Profiles { get; set; }

        string ExePath { get; set; }
    }
}
