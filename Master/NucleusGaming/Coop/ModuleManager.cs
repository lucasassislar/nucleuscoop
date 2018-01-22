using Nucleus.Gaming.Coop.Handler.Cursor;
using Nucleus.Gaming.Coop.Modules;
using Nucleus.Gaming.Platform.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Coop
{
    public delegate bool IsModuleNeeded(HandlerData data);

    public struct ModuleInfo
    {
        public IsModuleNeeded IsNeeded;
        public Type ModuleType;

        public ModuleInfo(IsModuleNeeded callback, Type moduleType)
        {
            IsNeeded = callback;
            ModuleType = moduleType;
        }
    }

    public class ModuleManager
    {
        private List<ModuleInfo> modules;
        public List<ModuleInfo> Modules
        {
            get { return modules; }
        }

        public ModuleManager()
        {
            modules = new List<ModuleInfo>();

            modules.Add(new ModuleInfo(CursorModule.IsNeeded, typeof(CursorModule)));
            modules.Add(new ModuleInfo(MutexModule.IsNeeded, typeof(MutexModule)));
            modules.Add(new ModuleInfo(GameProcessModule.IsNeeded, typeof(GameProcessModule)));
            modules.Add(new ModuleInfo(IOModule.IsNeeded, typeof(IOModule)));
            modules.Add(new ModuleInfo(XInputHandlerModule.IsNeeded, typeof(XInputHandlerModule)));
        }
    }
}
