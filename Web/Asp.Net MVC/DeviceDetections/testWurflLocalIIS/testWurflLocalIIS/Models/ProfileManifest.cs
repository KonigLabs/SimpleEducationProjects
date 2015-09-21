using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testWurflLocalIIS.Models
{
    public class ProfileManifest
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public string Version { get; set; }
        public Feature[] Features { get; set; }
    }
}