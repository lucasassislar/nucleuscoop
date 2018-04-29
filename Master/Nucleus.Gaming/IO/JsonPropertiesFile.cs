using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming.IO
{
    public class JsonPropertiesFile
    {
        private object locker = new object();
        protected string pathToFile;

        public JsonPropertiesFile(string _pathToFile)
        {
            this.pathToFile = _pathToFile;
        }

        public void Load()
        {
            lock (locker)
            {
                if (File.Exists(pathToFile))
                {
                    string jsonText = File.ReadAllText(pathToFile);
                    JsonConvert.PopulateObject(jsonText, this);
                }
            }
        }

        public void Save(string location = "")
        {
            if (!string.IsNullOrEmpty(location))
            {
                pathToFile = location;
            }

            lock (locker)
            {
                string serialized = JsonConvert.SerializeObject(this);
                if (File.Exists(pathToFile))
                {
                    File.Delete(pathToFile);
                }
                File.WriteAllText(pathToFile, serialized);
            }
        }
    }
}
