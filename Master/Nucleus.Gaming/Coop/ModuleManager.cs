using Nucleus.Gaming.Coop.Modules;
using Nucleus.Gaming.Platform.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus.Gaming.Coop {
    /// <summary>
    /// If the module needs to be used for the specified handler data (play session)
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public delegate bool IsModuleNeeded(HandlerData data);

    public struct ModuleInfo {
        public IsModuleNeeded IsNeeded;
        public Type ModuleType;

        public ModuleInfo(IsModuleNeeded callback, Type moduleType) {
            IsNeeded = callback;
            ModuleType = moduleType;
        }
    }

    public class ModuleManager {
        public List<ModuleInfo> Modules { get; private set; }

        public ModuleManager() {
            Modules = new List<ModuleInfo>();

            Modules.Add(new ModuleInfo(CursorModule.IsNeeded, typeof(CursorModule)));
            Modules.Add(new ModuleInfo(MutexModule.IsNeeded, typeof(MutexModule)));
            Modules.Add(new ModuleInfo(WindowsGameProcessModule.IsNeeded, typeof(WindowsGameProcessModule)));
            Modules.Add(new ModuleInfo(IOModule.IsNeeded, typeof(IOModule)));
            Modules.Add(new ModuleInfo(XInputHandlerModule.IsNeeded, typeof(XInputHandlerModule)));
        }
    }
}
