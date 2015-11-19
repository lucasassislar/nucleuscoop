using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace NukeUpdateWeb
{
    /// <summary>
    /// Summary description for version
    /// </summary>
    public class version : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string updateFolder = context.Server.MapPath("update");
            DirectoryInfo dir = new DirectoryInfo(updateFolder);
            FileInfo[] files = dir.GetFiles();

            string[] updates = new string[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                updates[i] = Path.GetFileNameWithoutExtension(files[i].Name);
            }

            context.Response.Write(JsonConvert.SerializeObject(updates));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}