using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    public static class StringUtil
    {
        public static readonly char[] Numbers = new char[]
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        public static bool IsNumber(char c)
        {
            // We COULD use Int.TryParse, but this looks way cleaner
            return Numbers.Contains(c);
        }
    }
}
