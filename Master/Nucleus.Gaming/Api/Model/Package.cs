using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;

namespace Nucleus.Gaming.Coop.Api
{
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
