using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SplitScreenMe {
    public interface IProcessData {
        Process Process { get; }

        IHwndObject HWnd { get; set; }
    }
}
