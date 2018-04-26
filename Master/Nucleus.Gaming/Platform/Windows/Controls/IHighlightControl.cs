using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Platform.Windows.Controls
{
    public interface IRadioControl
    {
        void RadioSelected();
        void RadioUnselected();

        // mouse enter basically
        void UserOver();
        // mouse leave
        void UserLeave();

        //void Highlight();
        //void SoftHighlight();
        //void Darken();
    }
}
