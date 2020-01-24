using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SplitScreenMe {
    public interface IUserScreen {
        Rectangle SwapTypeBounds { get; set; }

        Rectangle UIBounds { get; set; }
    }
}
