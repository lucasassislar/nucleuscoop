using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Windows
{
    /// <summary>
    /// Identifies dots per inch (dpi) awareness values. DPI awareness indicates how much scaling work an application performs for DPI versus how much is done by the system.
    /// Users have the ability to set the DPI scale factor on their displays independent of each other.Some legacy applications are not able to adjust their scaling for multiple DPI settings.In order for users to use these applications without content appearing too large or small on displays, Windows can apply DPI virtualization to an application, causing it to be automatically be scaled by the system to match the DPI of the current display.The PROCESS_DPI_AWARENESS value indicates what level of scaling your application handles on its own and how much is provided by Windows.Keep in mind that applications scaled by the system may appear blurry and will read virtualized data about the monitor to maintain compatibility.
    /// </summary>
    public enum ProcessDPIAwareness
    {
        /// <summary>
        /// DPI unaware. This app does not scale for DPI changes and is always assumed to have a scale factor of 100% (96 DPI). It will be automatically scaled by the system on any other DPI setting.
        /// </summary>
        ProcessDpiUnaware = 0,
        /// <summary>
        /// System DPI aware. This app does not scale for DPI changes. It will query for the DPI once and use that value for the lifetime of the app. If the DPI changes, the app will not adjust to the new DPI value. It will be automatically scaled up or down by the system when the DPI changes from the system value.
        /// </summary>
        ProcessSystemDPIAware = 1,
        /// <summary>
        /// Per monitor DPI aware. This app checks for the DPI when it is created and adjusts the scale factor whenever the DPI changes. These applications are not automatically scaled by the system.
        /// </summary>
        ProcessPerMonitorDPIAware = 2
    }
}
