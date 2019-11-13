using Microsoft.Win32;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Windows.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SplitScreenMe.Core {
    public static class SplitScreenEngineUtil {
        public static readonly string UriScheme = "splitme";
        public static readonly string FriendlyName = "SplitScreen Me";

        public static void HandleArguments(string[] args) {
            if (args != null) {
                for (int i = 0; i < args.Length; i++) {
                    string argument = args[i];
                    if (string.IsNullOrEmpty(argument)) {
                        continue;
                    }

                    string extension = Path.GetExtension(argument);
                    if (extension.ToLower().EndsWith("nc")) {
                        // try installing the package in the arguments if user allows it
                        //if (MessageBox.Show("Would you like to install " + argument + "?", "Question", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                        GameManager.Instance.PackageManager.InstallPackage(argument);
                        //}
                    }
                }
            }
        }

        public static void HandleRegisterUpdates() {
            GameManager gameManager = GameManager.Instance;
            if (!gameManager.User.Options.RequestedToAssociateFormat) {
                gameManager.User.Options.RequestedToAssociateFormat = true;

                //if (MessageBox.Show("Would you like to associate Nucleus Package Files (*.nc) and nuke:// links to the application?", "Question", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                string startLocation = Process.GetCurrentProcess().MainModule.FileName;
                if (!RegistryUtil.SetAssociation(".nc", "NucleusCoop", "Nucleus Package Files", startLocation)) {
                    //MessageBox.Show("Failed to set association");
                    //gameManager.User.Options.RequestedToAssociateFormat = false;
                }
                SplitScreenEngineUtil.RegisterUriScheme();

                gameManager.User.Save();
            }
        }

        public static void RegisterUriScheme() {
            using (var key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Classes\\" + UriScheme)) {
                string applicationLocation = Assembly.GetEntryAssembly().Location;

                key.SetValue("", "URL:" + FriendlyName);
                key.SetValue("URL Protocol", "");

                using (var defaultIcon = key.CreateSubKey("DefaultIcon")) {
                    defaultIcon.SetValue("", applicationLocation + ",1");
                }

                using (var commandKey = key.CreateSubKey(@"shell\open\command")) {
                    commandKey.SetValue("", "\"" + applicationLocation + "\" \"%1\"");
                }
            }
        }
    }
}
