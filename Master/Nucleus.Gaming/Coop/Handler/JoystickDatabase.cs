using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming
{
    /// <summary>
    /// this is ultimately wrong, we need to update x360ce - distrolucas
    /// </summary>
    public static class JoystickDatabase
    {
        public static Dictionary<string, int> JoystickIDs = new Dictionary<string, int>
        {
            { "05c4054c-0000-0000-0000-504944564944", 2 }
        };

        public static int GetID(string deviceGuid)
        {
            int id = 0;
            if (JoystickIDs.TryGetValue(deviceGuid, out id))
            {
                return id;
            }
            return 0;
        }
    }
}