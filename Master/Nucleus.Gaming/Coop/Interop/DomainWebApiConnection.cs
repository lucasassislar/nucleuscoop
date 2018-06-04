using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming.Coop.Interop
{
    public class DomainWebApiConnection : IDisposable
    {
        private AppDomain domain;
        private dynamic apiConnection;

        public string Token { get; private set; }

        public DomainWebApiConnection()
        {
            Evidence evidence = new Evidence();
            evidence.AddHostEvidence(new Zone(SecurityZone.Untrusted));

            PermissionSet permissionSet = new PermissionSet(PermissionState.None);
            permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));

            AppDomainSetup setup = new AppDomainSetup { ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase };
            domain = AppDomain.CreateDomain("WEBAPI", evidence, setup, permissionSet);

            string enginePath = GetLibraryPath();
            byte[] engineData = File.ReadAllBytes(enginePath);
            domain.Load(engineData);

            ObjectHandle apiObj = domain.CreateInstance("Nucleus.Gaming.Coop.Api", "Nucleus.Gaming.Coop.Api.ApiConnection");
            apiConnection = apiObj.Unwrap();
        }

        public static string GetLibraryPath()
        {
            return Path.Combine(AssemblyUtil.GetStartFolder(), "bin", "Nucleus.Gaming.Coop.Api.dll");
        }

        public void Dispose()
        {
            AppDomain.Unload(domain);
        }

        public void Initialize()
        {
            apiConnection.Initialize();
        }

        public String Register(string username, string email, string password, bool loginInSuccess = true)
        {
            dynamic data = apiConnection.Register(username, email, password);

            if (data.Success)
            {
                if (loginInSuccess)
                {
                    Login(data.email, data.password);
                }
                return data.Data.id;
            }
            return data.LogData;
        }

        public String Login(string email, string password)
        {
            dynamic data = apiConnection.Login(email, password);

            if (data.Success)
            {
                Token = data.Data.token;
                apiConnection.SetToken(Token);
                return Token;
            }
            return data.LogData;
        }
    }
}
