using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testWurflLocalIIS.Capabilities
{
    public static class HttpBrowserCapabilitiesExtensions
    {
        public static bool SupportsJSON(this HttpBrowserCapabilitiesBase httpBrowser)
        {
            return httpBrowser[AllCapabilities.JSON] == "1" ||
                httpBrowser[AllCapabilities.JSON] == "true";
        }

        public static bool SupportsDOMManipulation(this HttpBrowserCapabilitiesBase httpBrowser)
        {
            return httpBrowser[AllCapabilities.DOMManipulation] == "true";
        }

        public static bool SupportsHashChangeEvent(this HttpBrowserCapabilitiesBase httpBrowser)
        {
            return httpBrowser[AllCapabilities.HashChange] == "true";
        }

        public static bool IsWow(this HttpBrowserCapabilitiesBase httpBrowser)
        {
            // We should also check for supporting DOM manipulation; however,
            // we currently don't have a source for that particular capability.
            // If you use a third-party database for feature detection, then
            // you should consider adding a test for this.
            return httpBrowser.IsMobileDevice &&
                   httpBrowser.SupportsJSON() &&
                   httpBrowser.SupportsXmlHttp &&
                   httpBrowser.SupportsHashChangeEvent();
        }
    }
}