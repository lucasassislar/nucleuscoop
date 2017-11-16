using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Diagnostics
{
    public interface ILogNode
    {
        void OnFailureLog(StreamWriter writer);
    }
}
