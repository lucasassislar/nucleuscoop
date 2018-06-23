using Newtonsoft.Json;
using Nucleus.Gaming.Coop.Api;
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

        public bool IsOfflineMode { get; private set; }
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

        private RequestResult<T> ParseRequest<T>(RequestResult<String> request)
        {
            var result = new RequestResult<T>();
            result.SetStatus(request.Success);
            result.LogLine(request.LogData);
            if (!string.IsNullOrWhiteSpace(request.Data))
            {
                result.SetData(JsonConvert.DeserializeObject<T>(request.Data));
            }

            return result;
        }

        public void EnableOfflineMode()
        {
            IsOfflineMode = true;

            // remove token
            SetToken(string.Empty);
        }

        public void DisableOfflineMode()
        {
            IsOfflineMode = false;
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

        public void SetToken(string token)
        {
            Token = token;
            apiConnection.SetToken(token);
        }

        public async Task<RequestResult<User>> Register(string username, string email, string password)
        {
            return ParseRequest<User>(await (Task<RequestResult<String>>)apiConnection.Register(username, email, password));
        }

        public async Task<RequestResult<LoginData>> Login(string email, string password)
        {
            RequestResult<String> result = await (Task<RequestResult<String>>)apiConnection.Login(email, password);
            RequestResult<LoginData> loginData = ParseRequest<LoginData>(result);
            SetToken(loginData.Data.token);
            return loginData;
        }

        public async Task<RequestResult<IgdbGames>> SearchExtGame(string gameName)
        {
            return ParseRequest<IgdbGames>(await (Task<RequestResult<String>>)apiConnection.SearchExtGame(gameName));
        }
    }
}
