using System;
using System.Collections.Generic;

namespace SplitScreenMe.Core {
    public class CallbackData {
        private List<Action> callbacks;

        public CallbackData() {
            callbacks = new List<Action>();
        }

        public void Callback(Action action) {
            callbacks.Add(action);
        }

        public void Invoke() {
            for (int i = 0; i < callbacks.Count; i++) {
                callbacks[i].Invoke();
            }
        }
    }
}
