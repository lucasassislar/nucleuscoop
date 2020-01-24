using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SplitScreenMe {
    public interface IHwndObject {
        /// <summary>
        /// The windows handle to this object.
        /// </summary>
        IntPtr NativePtr { get; }

        /// <summary>
        /// The registered class name (if any) of this object.
        /// </summary>
        string ClassName { get; }

        /// <summary>
        /// The title of this object - Setting this will only effect window title-bar text.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// The text of this item - setting this will only effect controls and only with appropriate access/privacy
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// The location of this Hwnd Object.
        /// </summary>
        Point Location { get; set; }

        bool TopMost { get; set; }

        /// <summary>
        /// The size of this Hwnd Object.
        /// </summary>
        Size Size { get; set; }
    }
}
