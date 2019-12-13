using Newtonsoft.Json;
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

namespace SplitScreenMe.Core.Interop
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

            ObjectHandle apiObj = domain.CreateInstance("SplitScreenMe.Core.Api", "SplitScreenMe.Core.Api.ApiConnection");
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
            return Path.Combine(AssemblyUtil.GetStartFolder(), "bin", "SplitScreenMe.Core.Api.dll");
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

        public async Task<RequestResult<Game>> GetSpecificGameWithHandlers(string gameId)
        {
            return ParseRequest<Game>(await (Task<RequestResult<String>>)apiConnection.GetSpecificGameWithHandlers(gameId));
        }

        public async Task<RequestResult<List<Game>>> ListIntGames()
        {
            return ParseRequest<List<Game>>(await (Task<RequestResult<String>>)apiConnection.ListIntGames());
        }

        public async Task<RequestResult<GameHandler>> GetGameHandler(string handlerId)
        {
            return ParseRequest<GameHandler>(await (Task<RequestResult<string>>)apiConnection.GetHandler(handlerId));
        }

        public async Task<RequestResult<byte[]>> DownloadPackage(string handlerId, string packageId)
        {
            return await (Task<RequestResult<byte[]>>)apiConnection.DownloadPackage(handlerId, packageId);
        }
    }
}
