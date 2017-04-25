using Jint;
using Nucleus.Coop;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nucleus.Gaming
{
    public class GenericGameInfo// : IGenericGameInfo
    {
        private Engine engine;
        private string js;

        public XInputInfo XInput = new XInputInfo();
        public string[] DirSymlinkExclusions;
        public string[] FileSymlinkExclusions;
        public double HandlerInterval;
        public bool Debug;
        public bool SupportsPositioning;
        public bool SymlinkExe;
        public bool SupportsKeyboard;
        public string[] ExecutableContext;
        public string ExecutableName;
        public string SteamID;
        public string GUID;
        public string GameName;
        public int MaxPlayers;
        public GameOption[] Options;
        public int MaxPlayersOneMonitor;
        public SaveType SaveType;
        public string SavePath;
        public SaveInfo[] ModifySave;
        public string StartArguments;
        public string BinariesFolder;
        /// <summary>
        /// The relative path to where the games starts in
        /// </summary>
        public string WorkingFolder;
        public bool NeedsSteamEmulation;
        public string[] KillMutex;
        public string LauncherExe;
        public string LauncherTitle;
        public Action Play;

        public Type HandlerType
        {
            get { return typeof(GenericGameHandler); }
        }

        public Type[] AdditionalSteps
        {
            get
            {
                return new Type[]
                {
                    typeof(PlayerCountControl),
                    typeof(PositionsControl),
                    typeof(PlayerOptionsControl)
                };
            }
        }

        public GenericGameInfo(Stream str)
        {
            StreamReader reader = new StreamReader(str);
            js = reader.ReadToEnd();

            Assembly assembly = typeof(GameOption).Assembly;

            engine = new Engine(cfg => cfg.AllowClr(assembly));
            engine.SetValue("Game", this);
            engine.Execute("var Nucleus = importNamespace('Nucleus.Gaming');");
            engine.Execute(js);
            engine.SetValue("Game", (object)null);
        }

        public void PrePlay(GenericContext context, GenericGameHandler handler, PlayerInfo player)
        {
            engine.SetValue("Context", context);
            engine.SetValue("Handler", handler);
            engine.SetValue("Player", player);

            Play?.Invoke();
        }

        /// <summary>
        /// Clones this Game Info into a new Generic Context
        /// </summary>
        /// <returns></returns>
        public GenericContext CreateContext(GameProfile profile, PlayerInfo info, GenericGameHandler handler)
        {
            GenericContext context = new GenericContext(profile, info, handler);

            Type t = GetType();
            PropertyInfo[] props = t.GetProperties();

            Type c = context.GetType();
            PropertyInfo[] cprops = c.GetProperties();

            for (int i = 0; i < props.Length; i++)
            {
                PropertyInfo p = props[i];
                PropertyInfo d = cprops.FirstOrDefault(k => k.Name == p.Name);
                if (d == null)
                {
                    continue;
                }

                if (p.PropertyType != d.PropertyType ||
                    !d.CanWrite)
                {
                    continue;
                }

                object value = p.GetValue(this, null);
                d.SetValue(context, value, null);
            }

            return context;
        }
    }
}
