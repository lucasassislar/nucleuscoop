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
        protected UserGameInfo game;

        public virtual bool CanProceed { get { throw new NotImplementedException(); } }
        public virtual bool CanPlay { get { throw new NotImplementedException(); } }

        public virtual string Title { get { throw new NotImplementedException(); } }

        public GameProfile Profile { get { return profile; } }

        public event Action<UserControl, bool> OnCanPlay;

        protected virtual void RemoveFlicker()
        {
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer,
                true);
        }

        public virtual void Initialize(UserGameInfo game, GameProfile profile)
        {
            this.profile = profile;
            this.game = game;
        }

        protected virtual void OnCanPlayTrue(bool autoProceed)
        {
            if (OnCanPlay != null)
            {
                OnCanPlay(this, autoProceed);
            }
        }
    }
}
