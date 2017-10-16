using Jint;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming.IO
{
    public class JsPropertiesFile
    {
        private Engine engine;

        public JsPropertiesFile()
        {
        }
        public JsPropertiesFile(string pathToFile)
        {
            InitializeEngine(pathToFile);
        }

        private void InitializeEngine(string pathToFile)
        {
            Assembly assembly = this.GetType().Assembly;
            string str = File.ReadAllText(pathToFile);

            // major security hole :)
            engine = new Engine(cfg => cfg.AllowClr(assembly));
            engine.SetValue("config", this);

            engine.Execute(str);
        }
    }
}
