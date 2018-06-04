using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using RestSharp;

namespace Nucleus.Gaming.Coop.Api
{
    [Serializable]
    public class LoginData
    {
        public string token { get; set; }
        public User user { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    ///     
    /// 
    [Serializable]
    public class User
    {
        public int id { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string email { get; set; }
        public string username { get; set; }
    }

    [Serializable]
    public class Game
    {
        public int id { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string name { get; set; }
        public int igdb_id { get; set; }
        public float rating { get; set; }
        public string summary { get; set; }
        public string cover_igdb_id { get; set; }
        public List<Handler> handlers { get; set; }

        //public Handler CreateHandler(string handlerName, string handlerDetails)
        //{
        //    return (Handler)ApiController.CreateHandler(this.id, handlerName, handlerDetails);
        //}
    }

    [Serializable]
    public class Handler
    {
        public int id { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string name { get; set; }
        public string details { get; set; }
        public int currentVersion { get; set; }
        public int owner { get; set; }
        public int game { get; set; }

        //public Package CreatePackage(string packageFullPath, string packageInfos)
        //{
        //    return (Package)ApiController.CreatePackage(this.id, packageFullPath, packageInfos);
        //}
    }

    [Serializable]
    public class Package
    {
        public int id { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string details { get; set; }
        public int version { get; set; }
    }
}
