using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming
{
    public interface IUserInputForm
    {
        bool CanProceed { get; }
        bool CanPlay { get; }

        event Action Proceed;

        string Title { get; }

        void Initialize(UserGameInfo game, GameProfile profile);
    }
}
