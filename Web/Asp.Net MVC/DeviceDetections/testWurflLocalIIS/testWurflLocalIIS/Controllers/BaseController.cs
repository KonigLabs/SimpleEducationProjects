using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ScientiaMobile.WurflCloud;
using ScientiaMobile.WurflCloud.Config;
using testWurflLocalIIS.Models;

namespace testWurflLocalIIS.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            ViewBag.DeviceInfo = GetDataByRequest(HttpContext);
        }
        #region Определение с помощью облачных API -быстрый и простой способ
        public DeviceInfo GetDataByRequest(HttpContextBase context)
        {
            var config = new DefaultCloudClientConfig
            {
                ApiKey = ""//Тут APIkey, который получаете при регистрации нас сайте (всё бесплатно)
            };

            var manager = new CloudClientManager(config);

            var info = manager.GetDeviceInfo(context);

            return info.ConvertToKonigLabsDiviceInfo();
        }
        #endregion
    }
}