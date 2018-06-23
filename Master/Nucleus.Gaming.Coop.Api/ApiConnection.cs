//using RestSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming.Coop.Api
{
    [Serializable]
    public class ApiConnection
    {
        private String token;
        private Uri baseUri;

        public ApiConnection()
        {
        }

        public void Initialize()
        {
            baseUri = new Uri("https://api.nucleuscoop.com/");
        }

        public void SetToken(string token)
        {
            this.token = token;
        }

        private HttpWebRequest BuildRequest(Uri uri, HttpMethod method = null, object requestData = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            request.ContentType = "application/json";
            if (method == null)
            {
                request.Method = "GET";
            }
            else
            {
                request.Method = method.Method;
            }

            if (requestData != null)
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string serialized = JsonConvert.SerializeObject(requestData);

                    streamWriter.Write(serialized);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }

            return request;
        }

        private async Task<RequestResult<String>> ProcessResponse(HttpWebRequest request)
        {
            RequestResult<String> result = new RequestResult<string>();

            try
            {
                result.SetStatus(true);

                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var readResult = await streamReader.ReadToEndAsync();
                    result.SetData(readResult);
                }
            }
            catch (Exception ex)
            {
                result.SetStatus(false);
                result.SetLogData(ex.Message);

                // parse error message?
                //int errorPos = ex.Message.IndexOf(": (") + 3;
                //int endErrorPos = ex.Message.IndexOf(')', errorPos);
                //string errorCode = ex.Message.Substring(errorPos, endErrorPos - errorPos);
                //result.SetLogData(errorCode);
            }

            return result;
        }

        public async Task<RequestResult<String>> Register(string username, string email, string password)
        {
            Uri registerUri = new Uri(baseUri, "/auth/register");
            HttpWebRequest request = BuildRequest(registerUri, HttpMethod.Post, new { username, password, email });
            RequestResult<String> result = await ProcessResponse(request);

            return result;
        }

        public async Task<RequestResult<String>> Login(string email, string password)
        {
            Uri uri = new Uri(baseUri, "/auth/login");
            HttpWebRequest request = BuildRequest(uri, HttpMethod.Post, new { email, password });
            RequestResult<String> result = await ProcessResponse(request);

            return result;
        }

        public async Task<RequestResult<String>> SearchExtGame(string gameName)
        {
            Uri uri = new Uri(baseUri, $"game/api/search/{gameName}");
            HttpWebRequest request = BuildRequest(uri, HttpMethod.Get);
            request.Headers.Add("Authorization", "Bearer " + token);

            RequestResult<String> result = await ProcessResponse(request);

            return result;
        }

        public async Task<RequestResult<String>> ImportExtGame(string gameIGDBId)
        {
            Uri uri = new Uri(baseUri, $"game/api/import/{gameIGDBId}");
            HttpWebRequest request = BuildRequest(uri, HttpMethod.Get);
            request.Headers.Add("Authorization", "Bearer " + token);

            RequestResult<String> result = await ProcessResponse(request);

            return result;
        }

        public async Task<RequestResult<String>> ListIntGames()
        {
            Uri uri = new Uri(baseUri, $"game");
            HttpWebRequest request = BuildRequest(uri, HttpMethod.Get);

            RequestResult<String> result = await ProcessResponse(request);
            return result;
        }

        public async Task<RequestResult<String>> GetSpecificGameWithHandlers(int internalGameId)
        {
            Uri uri = new Uri(baseUri, $"game/{internalGameId}");
            HttpWebRequest request = BuildRequest(uri, HttpMethod.Get);

            RequestResult<String> result = await ProcessResponse(request);
            return result;

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

        public async Task<RequestResult<String>> CreateHandler(int gameId, string handlerName, string handlerDetails)
        {
            Uri uri = new Uri(baseUri, $"handler");
            HttpWebRequest request = BuildRequest(uri, HttpMethod.Post, new { game_id = gameId, name = handlerName, details = handlerDetails });
            request.Headers.Add("Authorization", "Bearer " + token);

            RequestResult<String> result = await ProcessResponse(request);

            //var request = new RestRequest("handler", Method.POST);
            //request.AddHeader("Authorization", "Bearer " + token);
            //request.AddJsonBody(new { game_id = gameId, name = handlerName, details = handlerDetails });
            //request.RequestFormat = DataFormat.Json;
            //IRestResponse<Handler> response = client.Execute<Handler>(request);
            //return response;
            return null;
        }

        //public IRestResponse<Package> CreatePackage(int handlerId, string packageFullPath, string packageInfos)
        //{
        //    //var request = new RestRequest("handler/{handlerId}", Method.POST);
        //    //request.AddHeader("Authorization", "Bearer " + token);
        //    //request.AddUrlSegment("handlerId", handlerId);
        //    //request.AddFile("package", @packageFullPath);
        //    //request.AddParameter("infos", packageInfos);
        //    //IRestResponse<Package> response = client.Execute<Package>(request);
        //    //return response;
        //    return null;
        //}
    }
}
