using System;
using System.Collections;
using System.Windows.Forms;

namespace Nucleus.Coop.App {
    public class EventCapture : IMessageFilter {
        public delegate void Callback(int message);
        public event Callback MessageReceived;

        IntPtr ownerWindow;
        Hashtable interestedMessages = null;
        public EventCapture(IntPtr handle, int[] messages) {
            ownerWindow = handle;
            interestedMessages = new Hashtable();
            for (int c = 0; c < messages.Length; c++) {
                interestedMessages[messages[c]] = 0;
            }
        }
        public bool PreFilterMessage(ref Message m) {
            if (m.HWnd == ownerWindow && interestedMessages.ContainsKey(m.Msg)) {
                MessageReceived(m.Msg);
            }
            return false;
        }

    }
}
