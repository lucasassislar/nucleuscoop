using Jint;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Win32;

namespace Nucleus.Gaming
{
    public class GenericGameInfo
    {
        private Engine engine;
        private string js;
        
        public GameHookInfo Hook = new GameHookInfo();
        public GameOption[] Options = new GameOption[0];

        public SaveType SaveType;
        public string SavePath;

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
        public int MaxPlayersOneMonitor;
        public int PauseBetweenStarts;

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
        public Action SetupSse;
        public List<CustomStep> CustomSteps = new List<CustomStep>();
        public string JsFileName;
        public bool LockMouse;

        public Type HandlerType
        {
            get { return typeof(GenericGameHandler); }
        }

        public GenericGameInfo(string fileName, Stream str)
        {
            JsFileName = fileName;

            StreamReader reader = new StreamReader(str);
            js = reader.ReadToEnd();

            Assembly assembly = typeof(GameOption).Assembly;

            engine = new Engine(cfg => cfg.AllowClr(assembly));
            engine.SetValue("Game", this);
            engine.Execute("var Nucleus = importNamespace('Nucleus.Gaming');");
            engine.Execute(js);
            engine.SetValue("Game", (object)null);
        }


        public CustomStep ShowOptionAsStep(string optionKey, bool required, string title)
        {
            GameOption option = Options.First(c => c.Key == optionKey);
            option.Hidden = true;

            CustomStep step = new CustomStep();
            step.Option = option;
            step.Required = required;
            step.Title = title;

            CustomSteps.Add(step);
            return step;
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
            FieldInfo[] fields = t.GetFields();

            Type c = context.GetType();
            PropertyInfo[] cprops = c.GetProperties();
            FieldInfo[] cfields = c.GetFields();

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

            for (int i = 0; i < fields.Length; i++)
            {
                FieldInfo source = fields[i];
                FieldInfo dest = cfields.FirstOrDefault(k => k.Name == source.Name);
                if (dest == null)
                {
                    continue;
                }

                if (source.FieldType != dest.FieldType)
                {
                    continue;
                }

                object value = source.GetValue(this);
                dest.SetValue(context, value);
            }

            return context;
        }

        public string GetSteamLanguage()
        {
            string result;
            if (Environment.Is64BitOperatingSystem)
                result = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Valve\Steam", "Language", null);
            else
                result = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam", "Language", null);

            return result;
        }
    }
}
