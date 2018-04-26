using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Windows
{
    /// <summary>
    /// Identifies the dots per inch (dpi) setting for a monitor.
    /// </summary>
    public enum MonitorDpiType : uint
    {
        /// <summary>
        /// The effective DPI. This value should be used when determining the correct scale factor for scaling UI elements. This incorporates the scale factor set by the user for this specific display.
        /// </summary>
        EffectiveDPIOrDefault = 0,
        /// <summary>
        /// The angular DPI. This DPI ensures rendering at a compliant angular resolution on the screen. This does not include the scale factor set by the user for this specific display.
        /// </summary>
        AngularDPI = 1,
        /// <summary>
        /// The raw DPI. This value is the linear DPI of the screen as measured on the screen itself. Use this value when you want to read the pixel density and not the recommended scaling setting. This does not include the scale factor set by the user for this specific display and is not guaranteed to be a supported DPI value.
        /// </summary>
        RawDPI = 2,
    }
}
