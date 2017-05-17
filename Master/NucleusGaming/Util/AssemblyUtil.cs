using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nucleus.Gaming
{
    public static class AssemblyUtil
    {
        public static string GetStartFolder()
        {
            Assembly entry = Assembly.GetEntryAssembly();
            return Path.GetDirectoryName(entry.Location);
        }
    }
}
