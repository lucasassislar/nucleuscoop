using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    public class RequestResult<T>
    {
        public T Data;
        public bool Success;
        public bool Failed;

        public string LogData;
        public object AdditionalData;
    }
}
