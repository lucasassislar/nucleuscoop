using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    public interface ILogNode
    {
        void Log(StreamWriter writer);
    }
}
