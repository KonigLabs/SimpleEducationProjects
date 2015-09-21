using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace testWurflLocalIIS.Services
{
    public class ProfileCookieEncoder : IProfileCookieEncoder
    {
        public IDictionary<string, string> GetDeviceCapabilities(HttpCookie profileCookie)
        {
            var value = HttpUtility.UrlDecode(profileCookie.Value);

            try
            {
                // Parses the http cookie with the device capabilities that
                // was encoded as json
                var serializer = new JavaScriptSerializer();
                var clientProfile = serializer.DeserializeObject(value) as IDictionary<string, object>;

                var capabilities = new Dictionary<string, string>();
                foreach (var key in clientProfile.Keys)
                {
                    capabilities.Add(key, clientProfile[key].ToString());
                }

                return capabilities;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("The profile cookie could not be parsed", ex);
            }
        }
    }
    public interface IProfileCookieEncoder
    {
        IDictionary<string, string> GetDeviceCapabilities(HttpCookie profileCookie);
    }
}