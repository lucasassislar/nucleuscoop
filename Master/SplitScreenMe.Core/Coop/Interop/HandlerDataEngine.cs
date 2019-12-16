using Newtonsoft.Json;
using Nucleus.Gaming;
using Nucleus.Gaming.Package;
using Nucleus.Gaming.Util;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;

namespace SplitScreenMe.Core.Interop {
    // TODO: rework this class
    public class HandlerDataEngine : IDisposable {
        private AppDomain domain;
        private dynamic jsEngine;
        private GameHandlerMetadata metadata;
        private string jsCode;

        public static string GetLibraryPath() {
            return Path.Combine(AssemblyUtil.GetStartFolder(), "bin", "SplitScreenMe.Engine.dll");
        }

        public HandlerDataEngine(GameHandlerMetadata metadata, string jsCode) {
            this.metadata = metadata;
            this.jsCode = jsCode;

            string tempPath = GameManager.GetTempFolder(metadata.GameID);
            Assembly platform = Assembly.GetExecutingAssembly();

            Evidence evidence = new Evidence();
            evidence.AddHostEvidence(new Zone(SecurityZone.Untrusted));

            PermissionSet permissionSet = new PermissionSet(PermissionState.None);

            permissionSet.AddPermission(new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery | FileIOPermissionAccess.Write, tempPath));
            permissionSet.AddPermission(new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, metadata.RootDirectory));
            permissionSet.AddPermission(new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, AssemblyUtil.GetStartFolder()));
            permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));

            AppDomainSetup setup = new AppDomainSetup { ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase };

            domain = AppDomain.CreateDomain("JSENGINE", evidence, setup, permissionSet);

            string enginePath = GetLibraryPath();
            byte[] engineData = File.ReadAllBytes(enginePath);
            domain.Load(engineData);

            ObjectHandle jsobj = domain.CreateInstance("SplitScreenMe.Engine", "SplitScreenMe.Engine.AppDomainEngine");
            jsEngine = jsobj.Unwrap();
            // TODO: strong typing on dynamic object (cache the fields/use reflection)
        }

        public void Dispose() {
            AppDomain.Unload(domain);
        }

        public string Initialize() {
            string metadataSerialized = JsonConvert.SerializeObject(metadata);
            string handlerData = jsEngine.Initialize(metadataSerialized, metadata.RootDirectory, jsCode);
            return handlerData;
        }

        public string Play(HandlerContext context, PlayerInfo player) {
            string contextData = JsonConvert.SerializeObject(context);
            string playerData = JsonConvert.SerializeObject(player);
            return jsEngine.Play(contextData, playerData);
        }
    }
}
