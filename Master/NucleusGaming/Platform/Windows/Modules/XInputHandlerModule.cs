using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.Handler;
using Nucleus.Gaming.Coop.Modules;
using Nucleus.Gaming.Platform.Windows.Interop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Platform.Windows
{
    public class XInputHandlerModule : HandlerModule
    {
        private GameHandler handler;
        private UserGameInfo userGame;
        private GameProfile profile;
        private HandlerData handlerData;

        public override int Order { get { return 1000; } }

        public override bool Initialize(GameHandler handler, HandlerData handlerData, UserGameInfo game, GameProfile profile)
        {
            this.handler = handler;
            this.userGame = game;
            this.profile = profile;
            this.handlerData = handlerData;

            return true;
        }

        public override void PrePlay()
        {
        }

        public override void PrePlayPlayer(PlayerInfo playerInfo, int index, HandlerContext context)
        {
        }

        public override void PlayPlayer(PlayerInfo playerInfo, int index, HandlerContext context)
        {
            if (!context.Hook.CustomDllEnabled)
            {
                return;
            }

            IOModule ioModule = handler.GetModule<IOModule>();

            byte[] xdata = Properties.Resources.xinput1_3;
            if (context.Hook.XInputNames == null)
            {
                using (Stream str = File.OpenWrite(Path.Combine(ioModule.LinkedBinFolder, "xinput1_3.dll")))
                {
                    str.Write(xdata, 0, xdata.Length);
                }
            }
            else
            {
                string[] xinputs = context.Hook.XInputNames;
                for (int z = 0; z < xinputs.Length; z++)
                {
                    string xinputName = xinputs[z];
                    using (Stream str = File.OpenWrite(Path.Combine(ioModule.LinkedBinFolder, xinputName)))
                    {
                        str.Write(xdata, 0, xdata.Length);
                    }
                }
            }

            Rectangle playerBounds = playerInfo.MonitorBounds;

            string ncoopIni = Path.Combine(ioModule.LinkedBinFolder, "ncoop.ini");
            using (Stream str = File.OpenWrite(ncoopIni))
            {
                byte[] ini = Properties.Resources.ncoop;
                str.Write(ini, 0, ini.Length);
            }

            IniFile x360 = new IniFile(ncoopIni);
            x360.IniWriteValue("Options", "ForceFocus", handlerData.Hook.ForceFocus.ToString(CultureInfo.InvariantCulture));
            x360.IniWriteValue("Options", "ForceFocusWindowName", handlerData.Hook.ForceFocusWindowName.ToString(CultureInfo.InvariantCulture));

            x360.IniWriteValue("Options", "WindowX", playerBounds.X.ToString(CultureInfo.InvariantCulture));
            x360.IniWriteValue("Options", "WindowY", playerBounds.Y.ToString(CultureInfo.InvariantCulture));

            if (context.Hook.SetWindowSize)
            {
                x360.IniWriteValue("Options", "ResWidth", context.Width.ToString(CultureInfo.InvariantCulture));
                x360.IniWriteValue("Options", "ResHeight", context.Height.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                x360.IniWriteValue("Options", "ResWidth", "0");
                x360.IniWriteValue("Options", "ResHeight", "0");
            }

            x360.IniWriteValue("Options", "RerouteInput", context.Hook.XInputReroute.ToString(CultureInfo.InvariantCulture));
            x360.IniWriteValue("Options", "RerouteJoystickTemplate", JoystickDatabase.GetID(playerInfo.GamepadProductGuid.ToString()).ToString(CultureInfo.InvariantCulture));

            x360.IniWriteValue("Options", "EnableMKBInput", playerInfo.IsKeyboardPlayer.ToString(CultureInfo.InvariantCulture));

            // windows events
            x360.IniWriteValue("Options", "BlockInputEvents", context.Hook.BlockInputEvents.ToString(CultureInfo.InvariantCulture));
            x360.IniWriteValue("Options", "BlockMouseEvents", context.Hook.BlockMouseEvents.ToString(CultureInfo.InvariantCulture));
            x360.IniWriteValue("Options", "BlockKeyboardEvents", context.Hook.BlockKeyboardEvents.ToString(CultureInfo.InvariantCulture));

            // xinput
            x360.IniWriteValue("Options", "XInputEnabled", context.Hook.XInputEnabled.ToString(CultureInfo.InvariantCulture));
            x360.IniWriteValue("Options", "XInputPlayerID", playerInfo.GamepadId.ToString(CultureInfo.InvariantCulture));

            // dinput
            x360.IniWriteValue("Options", "DInputEnabled", context.Hook.DInputEnabled.ToString(CultureInfo.InvariantCulture));
            x360.IniWriteValue("Options", "DInputGuid", playerInfo.GamepadGuid.ToString().ToUpper());
            x360.IniWriteValue("Options", "DInputForceDisable", context.Hook.DInputForceDisable.ToString());
        }

        public static bool IsNeeded(HandlerData data)
        {
#if WINDOWS
            return true;
#else
            return false;
#endif
        }

        public override void Tick(double delayMs)
        {
        }
    }
}
