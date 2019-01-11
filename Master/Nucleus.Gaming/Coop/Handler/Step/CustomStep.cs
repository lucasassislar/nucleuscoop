using Nucleus.Gaming.Coop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Coop.Handler
{
    /// <summary>
    /// Custom step for the final user to grab information for a specified Game Option
    /// </summary>
    public class CustomStep
    {
        public GameOption Option;
        public string Title;

        public bool Required;
    }
}
