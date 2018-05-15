using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using RestSharp;

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

            //We can map automagically Json answers to classes. Uncomment the two following comments to see the result.

            //IRestResponse<User> response = client.Execute<User>(request);
            IRestResponse response = client.Execute(request);
            return response.Content;
            //return response.Data.username;
        }
        public static String Login(string email, string password)
        {
            var request = new RestRequest("auth/login", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new { email, password });
            IRestResponse response = client.Execute(request);
            return response.Content;
        }
        public static String Search(string gameName)
        {
            var request = new RestRequest("game/api/search/{gameName}", Method.GET);
            request.AddHeader("Authorization", "Bearer " + Token);
            request.AddUrlSegment("gameName", gameName);
            request.RequestFormat = DataFormat.Json;

            IRestResponse response = client.Execute(request);
            return response.Content;
        }
    }
}
