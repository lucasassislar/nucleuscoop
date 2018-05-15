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
    public class User
    {
    public int id { get; set; }
    public string createdAt { get; set; }
    public string updatedAt { get; set; }
    public string email { get; set; }
    public string username { get; set; }
    }

    public class Game
    {
        public string id { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        // To be continued.
    }

}
