using Nucleus.Gaming.Coop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming {
    public interface IGameProcessModule {
        bool HasWindowSetup(PlayerInfo info);
    }
}
