using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Platform.Windows.Controls
{
    public interface ICanProceed
    {
        bool CanProceed { get; }
        bool AutoProceed { get; }
        void AutoProceeded();

        string StepTitle { get; }
        void Restart();
    }
}
