using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    public abstract class GameOptionValue
    {
        private string name;
        private int value;

        public string Name
        {
            get { return name; }
        }

        public int Value
        {
            get { return value; }
        }

        public GameOptionValue(string nam, int val)
        {
            name = nam;
            value = val;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
