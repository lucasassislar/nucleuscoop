using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FocusFakerEnabler
{
    class Program
    {
        [DllImport("FocusFaker.dll", EntryPoint = "_InstallHook@0")]
        public static extern bool InstallHook();

        [DllImport("FocusFaker.dll", EntryPoint = "_RemoveHook@0")]
        public static extern bool RemoveHook();

        static void Main(string[] args)
        {
            bool installed = InstallHook();

            if (installed)
            {
                Console.WriteLine("Global hook installed");
            }
            else
            {
                Console.WriteLine("Global hook not installed");
            }

            Console.ReadLine();

            installed = RemoveHook();
            if (installed)
            {
                Console.WriteLine("Global hook removed");
            }
            else
            {
                Console.WriteLine("Global hook not removed");
            }
            Console.ReadLine();
        }
    }
}
