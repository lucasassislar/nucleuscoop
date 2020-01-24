using System;
using System.Reflection;

namespace SplitScreenMe {
    public class Engine {
        public string Path { get; private set; }

        private Assembly engineAssembly;

        public Engine(string path) {
            Path = path;
        }

        public void Initialize() {
            engineAssembly = Assembly.LoadFile(this.Path);
        }
    }
}
