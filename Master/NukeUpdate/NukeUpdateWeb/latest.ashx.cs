using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace NukeUpdateWeb
{
    /// <summary>
    /// Summary description for latest
    /// </summary>
    public class latest : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string updateFolder = context.Server.MapPath("update");
            DirectoryInfo dir = new DirectoryInfo(updateFolder);
            var files = dir.GetFiles().OrderByDescending(c => c.LastWriteTime);

            context.Response.Write(files.First().Name);
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