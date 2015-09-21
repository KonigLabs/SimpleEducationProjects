using System;
using System.Web.Script.Serialization;

namespace testWurflLocalIIS.Models
{
    public class DeviceInfo
    {
        public string DeviceOS { get; set; }
        public Version DeviceOSVersion { get; set; }
        public string ModelName { get; set; }
        public string UserAgent { get; set; }
        public bool IsTablet { get; set; }
        public bool HasCookieSupport { get; set; }
        public bool HasHardwareBack
        {
            get
            {
                if (DeviceOS == "Android")
                    return true;
                else
                    return false;
            }
        }
        public string ToJson()
        {
            var data = new
            {
                deviceOS = DeviceOS,
                deviceOSVersion = DeviceOSVersion.ToString(),
                modelName = ModelName,
                isTablet = IsTablet,
                hasHardwareBack = HasHardwareBack,
                hasCookieSupport = HasCookieSupport
            };
            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(data);
            return json;
        }
    }
}