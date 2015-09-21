using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testWurflLocalIIS.Models
{
    public static class Extensions
    {
        public static DeviceInfo ConvertToKonigLabsDiviceInfo(this ScientiaMobile.WurflCloud.Device.DeviceInfo device)
        {
            var deviceInfo = new DeviceInfo();
            bool isTablet = false;
            string strIsTablet = device.Capabilities["is_tablet"];
            Boolean.TryParse(strIsTablet, out isTablet);
            deviceInfo.IsTablet = isTablet;
            return deviceInfo;
        }
    }
}