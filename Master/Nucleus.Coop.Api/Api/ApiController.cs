using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using RestSharp;
using System.IO;

namespace Nucleus.Coop.Api
{
    /// <summary>
    /// 
    /// </summary>
    ///     
    /// 

public class ApiController : UserControl
    {
        public static RestClient client = new RestClient("https://api.nucleuscoop.com/");
        public static String Token { get; set; }

        public static String Register(string username, string email, string password)
        {
            var request = new RestRequest("auth/register", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new { username, password, email });

            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public static String Login(string email, string password)
        {
            var request = new RestRequest("auth/login", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new { email, password });
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public static String SearchExtGame(string gameName)
        {
            var request = new RestRequest("game/api/search/{gameName}", Method.GET);
            request.AddHeader("Authorization", "Bearer " + Token);
            request.AddUrlSegment("gameName", gameName);
            request.RequestFormat = DataFormat.Json;

            IRestResponse response = client.Execute(request);
            return response.Content;
        }


        public static String ImportExtGame(string gameIGDBId)
        {
            var request = new RestRequest("game/api/import/{gameIGDBId}", Method.POST);
            request.AddHeader("Authorization", "Bearer " + Token);
            request.AddUrlSegment("gameIGDBId", gameIGDBId);
            request.RequestFormat = DataFormat.Json;

            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public static IRestResponse<List<Game>> ListIntGames()
        {
            var request = new RestRequest("game", Method.GET);
            request.RequestFormat = DataFormat.Json;

            IRestResponse<List<Game>> response = client.Execute<List<Game>>(request);
            return response;
        }

        public static IRestResponse<Game> GetSpecificGameWithHandlers(int internalGameId)
        {
            var request = new RestRequest("game/{internalGameId}", Method.GET);
            request.AddUrlSegment("internalGameId", internalGameId);
            request.RequestFormat = DataFormat.Json;

            IRestResponse<Game> response = client.Execute<Game>(request);
            return response;
        }


        public static IRestResponse<Handler> CreateHandler(int gameId, string handlerName, string handlerDetails)
        {
            var request = new RestRequest("handler", Method.POST);
            request.AddHeader("Authorization", "Bearer " + Token);
            request.AddJsonBody(new { game_id = gameId, name = handlerName, details = handlerDetails });
            request.RequestFormat = DataFormat.Json;
            IRestResponse<Handler> response = client.Execute<Handler>(request);
            return response;
        }

        public static IRestResponse<Package> CreatePackage(int handlerId, string packageFullPath, string packageInfos)
        {
            var request = new RestRequest("handler/{handlerId}", Method.POST);
            request.AddHeader("Authorization", "Bearer " + Token);
            request.AddUrlSegment("handlerId", handlerId);
            request.AddFile("package", @packageFullPath);
            request.AddParameter("infos", packageInfos);
            IRestResponse<Package> response = client.Execute<Package>(request);
            return response;
        }

        public static bool GetPackage(int handlerId, int specificVersion = -1)
        {
            var request = new RestRequest("handler/{handlerId}/{specificVersion}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("handlerId", handlerId);
            request.AddUrlSegment("specificVersion", specificVersion >= 1 ? Convert.ToString(specificVersion) : "latest");
            var fileBytes = client.DownloadData(request);
            File.WriteAllBytes("package.nc", fileBytes);
            return true; // As for now. Should be correctly wrapped (and return false or errorcode on error...)
        }
    }
}
