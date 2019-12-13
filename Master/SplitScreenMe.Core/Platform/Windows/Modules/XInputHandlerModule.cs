using SplitScreenMe.Core;
using SplitScreenMe.Core.Handler;
using SplitScreenMe.Core.Modules;
using Nucleus.Gaming.Platform.Windows.Interop;
using System.Drawing;
using System.Globalization;
using System.IO;
using SplitScreenMe.Core.Properties;

namespace Nucleus.Gaming.Platform.Windows {
    /// <summary>
    /// x360ce fork / xinput handling module
    /// </summary>
    public class XInputHandlerModule : HandlerModule {
        private GameHandler handler;
        private UserGameInfo userGame;
        private GameProfile profile;
        private HandlerData handlerData;

        public override int Order { get { return 20; } }

        public XInputHandlerModule(PlayerInfo player)
            : base(player) {
        }

        public override bool Initialize(GameHandler handler, HandlerData handlerData, UserGameInfo game, GameProfile profile) {
            this.handler = handler;
            this.userGame = game;
            this.profile = profile;
            this.handlerData = handlerData;
            return true;
        }

        public override void PrePlayPlayer(int index, HandlerContext context) {
        }

        public override void PlayPlayer(int index, HandlerContext context) {
            if (!context.Hook.CustomDllEnabled) {
                return;
            }

            IOModule ioModule = handler.GetModule<IOModule>(Player);

            byte[] xdata = Resources.xinput1_3;
            if (context.Hook.XInputNames == null) {
                using (Stream str = File.OpenWrite(Path.Combine(ioModule.LinkedWorkingDir, "xinput1_3.dll"))) {
                    str.Write(xdata, 0, xdata.Length);
                }
            } else {
                string[] xinputs = context.Hook.XInputNames;
                for (int z = 0; z < xinputs.Length; z++) {
                    string xinputName = xinputs[z];
                    using (Stream str = File.OpenWrite(Path.Combine(ioModule.LinkedWorkingDir, xinputName))) {
                        str.Write(xdata, 0, xdata.Length);
                    }
                }
            }

            Rectangle playerBounds = Player.MonitorBounds;

            string ncoopIni = Path.Combine(ioModule.LinkedWorkingDir, "ncoop.ini");
            using (Stream str = File.OpenWrite(ncoopIni)) {
                byte[] ini = Resources.ncoop;
                str.Write(ini, 0, ini.Length);
            }

            IniFile x360 = new IniFile(ncoopIni);
            x360.IniWriteValue("Options", "ForceFocus", handlerData.Hook.ForceFocus.ToString(CultureInfo.InvariantCulture));
            x360.IniWriteValue("Options", "ForceFocusWindowRegex", handlerData.Hook.ForceFocusWindowRegex.ToString(CultureInfo.InvariantCulture));

            x360.IniWriteValue("Options", "WindowX", playerBounds.X.ToString(CultureInfo.InvariantCulture));
            x360.IniWriteValue("Options", "WindowY", playerBounds.Y.ToString(CultureInfo.InvariantCulture));

            x360.IniWriteValue("Options", "ResWidth", context.Width.ToString(CultureInfo.InvariantCulture));
            x360.IniWriteValue("Options", "ResHeight", context.Height.ToString(CultureInfo.InvariantCulture));

            //x360.IniWriteValue("Options", "FixResolution", context.Hook.SetWindowSize.ToString(CultureInfo.InvariantCulture));
            //x360.IniWriteValue("Options", "FixPosition", context.Hook.SetWindowPosition.ToString(CultureInfo.InvariantCulture));

            x360.IniWriteValue("Options", "FixResolution", (true).ToString(CultureInfo.InvariantCulture));
            x360.IniWriteValue("Options", "FixPosition", (true).ToString(CultureInfo.InvariantCulture));
            x360.IniWriteValue("Options", "ClipMouse", Player.IsKeyboardPlayer.ToString(CultureInfo.InvariantCulture)); //context.Hook.ClipMouse

            x360.IniWriteValue("Options", "RerouteInput", context.Hook.XInputReroute.ToString(CultureInfo.InvariantCulture));
            x360.IniWriteValue("Options", "RerouteJoystickTemplate", JoystickDatabase.GetID(Player.GamepadProductGuid.ToString()).ToString(CultureInfo.InvariantCulture));

            x360.IniWriteValue("Options", "EnableMKBInput", Player.IsKeyboardPlayer.ToString(CultureInfo.InvariantCulture));
            x360.IniWriteValue("Options", "EnableMKBInput", Player.IsKeyboardPlayer.ToString(CultureInfo.InvariantCulture));

            // Windows events
            //x360.IniWriteValue("Options", "BlockInputEvents", context.Hook.BlockInputEvents.ToString(CultureInfo.InvariantCulture));
            //x360.IniWriteValue("Options", "BlockMouseEvents", context.Hook.BlockMouseEvents.ToString(CultureInfo.InvariantCulture));
            //x360.IniWriteValue("Options", "BlockKeyboardEvents", context.Hook.BlockKeyboardEvents.ToString(CultureInfo.InvariantCulture));

            // xinput
            x360.IniWriteValue("Options", "XInputEnabled", context.Hook.XInputEnabled.ToString(CultureInfo.InvariantCulture));
            x360.IniWriteValue("Options", "XInputPlayerID", Player.GamepadId.ToString(CultureInfo.InvariantCulture));

            // dinput
            x360.IniWriteValue("Options", "DInputEnabled", context.Hook.DInputEnabled.ToString(CultureInfo.InvariantCulture));
            x360.IniWriteValue("Options", "DInputGuid", Player.GamepadGuid.ToString().ToUpper());
            x360.IniWriteValue("Options", "DInputForceDisable", context.Hook.DInputForceDisable.ToString());
        }

        public static bool IsNeeded(HandlerData data) {
#if WINDOWS
            return true;
#else
            return false;
#endif
        }

        public override void Tick(double delayMs) {
        }
    }
}
