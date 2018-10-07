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
            //baseUri = new Uri("https://api.nucleuscoop.com/");
            baseUri = new Uri("http://127.0.0.1:1337/");
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

        private async Task<RequestResult<byte[]>> ProcessResponseRaw(HttpWebRequest request)
        {
            RequestResult<byte[]> result = new RequestResult<byte[]>();

            try
            {
                result.SetStatus(true);

                var httpResponse = (HttpWebResponse)request.GetResponse();
                Stream str = httpResponse.GetResponseStream();
                byte[] buffer = new byte[str.Length];
                await str.ReadAsync(buffer, 0, buffer.Length);

                result.SetData(buffer);
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

        public async Task<RequestResult<String>> GetHandler(int handlerId)
        {
            Uri uri = new Uri(baseUri, $"handler/{handlerId}");
            HttpWebRequest request = BuildRequest(uri, HttpMethod.Get);

            RequestResult<String> result = await ProcessResponse(request);
            return result;
        }

        public async Task<RequestResult<byte[]>> DownloadPackage(int handlerId, string specificVersion = "-1")
        {
            Uri uri = new Uri(baseUri, $"handler/{handlerId}/{(specificVersion == "-1" ? "latest" : specificVersion)}");
            HttpWebRequest request = BuildRequest(uri, HttpMethod.Get);

            RequestResult<byte[]> result = await ProcessResponseRaw(request);
            return result;
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
