using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nucleus.Gaming {
    public static class ThreadUtil {
        public static int MainThreadId { get; private set; }

        public static void Initialize() {
            MainThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        public static bool IsMainThread {
            get { return Thread.CurrentThread.ManagedThreadId == MainThreadId; }
        }
    }
}
