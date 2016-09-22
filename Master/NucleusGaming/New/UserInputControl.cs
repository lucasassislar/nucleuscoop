using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Gaming
{
    public class UserInputControl : UserControl
    {
        protected GameProfile profile;

        public virtual bool CanProceed { get { throw new NotImplementedException(); } }
        public virtual bool CanPlay { get { throw new NotImplementedException(); } }

        public virtual string Title { get { throw new NotImplementedException(); } }

        public GameProfile Profile { get { return profile; } }

        public event Action<UserControl> OnCanPlay;

        public virtual void Initialize(UserGameInfo game, GameProfile profile)
        {
            this.profile = profile;
        }

        protected virtual void OnCanPlayTrue()
        {
            if (OnCanPlay != null)
            {
                OnCanPlay(this);
            }
        }
    }
}
