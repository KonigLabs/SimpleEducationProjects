using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testWurflLocalIIS.Capabilities
{
    public static class AllCapabilities
    {
        public const string MobileDevice = "isMobileDevice";
        public const string DOMManipulation = "ajax_manipulate_dom";
        public const string JSON = "json";
        public const string HashChange = "hashchange";
        public const string Width = "screenPixelsWidth";
        public const int DefaultWidth = 320;
    }
}