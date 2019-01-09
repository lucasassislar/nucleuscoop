using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win32;

namespace Nucleus.Gaming.Platform.Windows.Controls
{
    public class MouseMessageFilter : IMessageFilter, IDisposable
    {
        public MouseMessageFilter()
        {
        }

        public void Dispose()
        {
            StopFiltering();
        }

        public bool PreFilterMessage(ref Message m)
        {
            // Call the appropriate event
            if (m.Msg == (int)Messages.WM_MOUSEMOVE)
            {
                IntPtr xy = m.LParam;
                int x = unchecked((short)(long)xy);
                int y = unchecked((short)((long)xy >> 16));

                if (MouseMove != null) {
                    MouseMove(this, new MouseEventArgs(MouseButtons.None, 0, x, y, 0));
                }
            } else if (m.Msg == (int)Messages.WM_LBUTTONDOWN) {
                if (MouseDown != null) {
                    MouseDown(this, new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
                }

            } else if (m.Msg == (int)Messages.WM_LBUTTONUP) {
                if (MouseUp != null) {
                    MouseUp(this, new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
                }
            } else if (m.Msg == (int)Messages.WM_RBUTTONDOWN) {
                if (MouseDown != null) {
                    MouseDown(this, new MouseEventArgs(MouseButtons.Right, 0, 0, 0, 0));
                }

            } else if (m.Msg == (int)Messages.WM_RBUTTONUP) {
                if (MouseUp != null) {
                    MouseUp(this, new MouseEventArgs(MouseButtons.Right, 0, 0, 0, 0));
                }
            }

            return false;
        }

        public delegate void CustomMouseEventHandler(object source, MouseEventArgs e);
        public event CustomMouseEventHandler MouseMove;
        public event CustomMouseEventHandler MouseDown;
        public event CustomMouseEventHandler MouseUp;

        public void StartFiltering()
        {
            StopFiltering();
            Application.AddMessageFilter(this);
        }

        public void StopFiltering()
        {
            Application.RemoveMessageFilter(this);
        }
    }
}
