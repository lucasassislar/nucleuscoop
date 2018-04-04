using Newtonsoft.Json;
using Nucleus.Gaming.Package;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;

namespace Nucleus.Gaming.Coop.JS
{
    public class HandlerDataJSEngine : IDisposable
    {
        private AppDomain domain;
        private AppDomainJSEngine jsEngine;
        private GameHandlerMetadata metadata;
        private string jsCode;

        public HandlerDataJSEngine(GameHandlerMetadata metadata, string jsCode)
        {
            this.metadata = metadata;
            this.jsCode = jsCode;

            string tempPath = GameManager.GetTempFolder(metadata.GameID);

            Assembly platform = Assembly.GetExecutingAssembly();

#if MAINTHREAD
            jsEngine = new AppDomainJSEngine();
#else

            Evidence evidence = new Evidence();
            evidence.AddHostEvidence(new Zone(SecurityZone.Untrusted));

            //PermissionSet permissionSet = SecurityManager.GetStandardSandbox(evidence);
            PermissionSet permissionSet = new PermissionSet(PermissionState.None);

            //permissionSet.SetPermission(new FileIOPermission(PermissionState.None));
            permissionSet.RemovePermission(typeof(FileIOPermission));

            //permissionSet.AddPermission(new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery | FileIOPermissionAccess.Write, tempPath));
            permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            AppDomainSetup setup = new AppDomainSetup { ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase };

            domain = AppDomain.CreateDomain("JSENGINE", null, setup, permissionSet);


            //ObjectHandle hobj = domain.CreateInstance("Nucleus.Gaming", "Nucleus.Gaming.Coop.JS.AppDomainData");
            //AppDomainData migrator = (AppDomainData)hobj.Unwrap();
            //migrator.ClassType = "";
            //migrator.Data = JsonConvert.SerializeObject(data);

            ObjectHandle jsobj = domain.CreateInstance("Nucleus.Gaming", "Nucleus.Gaming.Coop.JS.AppDomainJSEngine");
            jsEngine = (AppDomainJSEngine)jsobj.Unwrap();
#endif
        }

        public void Dispose()
        {
#if !MAINTHREAD
            AppDomain.Unload(domain);
#endif
        }

        public string Initialize()
        {
            string metadataSerialized = JsonConvert.SerializeObject(metadata);
            string handlerData = jsEngine.Initialize(metadataSerialized, metadata.RootDirectory, jsCode);
            return handlerData;
        }

        public string Play(HandlerContext context, PlayerInfo player)
        {
            string contextData = JsonConvert.SerializeObject(context);
            string playerData = JsonConvert.SerializeObject(player);
            return jsEngine.Play(contextData, playerData);
        }
    }
}
