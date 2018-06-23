using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            if (m.Msg == 0x200)
            {
                IntPtr lParam = m.LParam;
                long coords = lParam.ToInt64();
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
