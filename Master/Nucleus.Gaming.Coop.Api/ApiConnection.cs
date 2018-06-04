using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming.Coop.Api
{
    [Serializable]
    public class ApiConnection
    {
        private RestClient client;
        private String token;

        public ApiConnection()
        {
        }

        public void Initialize()
        {
            client = new RestClient("https://api.nucleuscoop.com/");
        }

        public void SetToken(string token)
        {
            this.token = token;
        }

        private RequestResult<T> BuildRequestResult<T>(IRestResponse<T> response)
        {
            var result = new RequestResult<T>();
            result.Success = response.IsSuccessful;
            result.Data = response.Data;
            result.LogData = response.StatusDescription;

            return result;
        }

        public RequestResult<User> Register(string username, string email, string password)
        {
            var request = new RestRequest("auth/register", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new { username, password, email });

            IRestResponse<User> response = client.Execute<User>(request);

            return BuildRequestResult<User>(response);
        }

        public RequestResult<LoginData> Login(string email, string password)
        {
            var request = new RestRequest("auth/login", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new { email, password });
            IRestResponse<LoginData> response = client.Execute<LoginData>(request);

            return BuildRequestResult<LoginData>(response);
        }

        public String SearchExtGame(string gameName)
        {
            var request = new RestRequest("game/api/search/{gameName}", Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddUrlSegment("gameName", gameName);
            request.RequestFormat = DataFormat.Json;

            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public String ImportExtGame(string gameIGDBId)
        {
            var request = new RestRequest("game/api/import/{gameIGDBId}", Method.POST);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddUrlSegment("gameIGDBId", gameIGDBId);
            request.RequestFormat = DataFormat.Json;

            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public List<Game> ListIntGames()
        {
            var request = new RestRequest("game", Method.GET);
            request.RequestFormat = DataFormat.Json;

            IRestResponse<List<Game>> response = client.Execute<List<Game>>(request);
            return response.Data;
        }

        public Game GetSpecificGameWithHandlers(int internalGameId)
        {
            var request = new RestRequest("game/{internalGameId}", Method.GET);
            request.AddUrlSegment("internalGameId", internalGameId);
            request.RequestFormat = DataFormat.Json;

            IRestResponse<Game> response = client.Execute<Game>(request);
            return response.Data;
        }

        public bool GetPackage(int handlerId, int specificVersion = -1)
        {
            return false;
            //var request = new RestRequest("handler/{handlerId}/{specificVersion}", Method.GET);
            //request.RequestFormat = DataFormat.Json;
            //request.AddUrlSegment("handlerId", handlerId);
            //request.AddUrlSegment("specificVersion", specificVersion >= 1 ? Convert.ToString(specificVersion) : "latest");
            //var fileBytes = client.DownloadData(request);
            //File.WriteAllBytes("package.nc", fileBytes);
            //return true; // As for now. Should be correctly wrapped (and return false or errorcode on error...)
        }

        public IRestResponse<Handler> CreateHandler(int gameId, string handlerName, string handlerDetails)
        {
            var request = new RestRequest("handler", Method.POST);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddJsonBody(new { game_id = gameId, name = handlerName, details = handlerDetails });
            request.RequestFormat = DataFormat.Json;
            IRestResponse<Handler> response = client.Execute<Handler>(request);
            return response;
        }

        public IRestResponse<Package> CreatePackage(int handlerId, string packageFullPath, string packageInfos)
        {
            var request = new RestRequest("handler/{handlerId}", Method.POST);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddUrlSegment("handlerId", handlerId);
            request.AddFile("package", @packageFullPath);
            request.AddParameter("infos", packageInfos);
            IRestResponse<Package> response = client.Execute<Package>(request);
            return response;
        }
    }
}
