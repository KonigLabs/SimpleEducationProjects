using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using testWurflLocalIIS.ClientProfile;
using testWurflLocalIIS.Models;
using testWurflLocalIIS.Services;

namespace testWurflLocalIIS.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        private static object ProfileLock = new object();

        IProfileManifestRepository _profileRepository;
        IProfileCookieEncoder _encoder;

        public ProfileController(IProfileManifestRepository profileRepository, IProfileCookieEncoder encoder)
        {
            _profileRepository = profileRepository;
            _encoder = encoder;
        }

        public ActionResult ProfileScript()
        {
            var model = GetProfile();

            var profileCookie = Request.Cookies["profile"];

            if (profileCookie != null)
            {
                var clientProfile = _encoder.GetDeviceCapabilities(profileCookie);

                if (clientProfile.ContainsKey("id"))
                {
                    var parts = clientProfile["id"].Split('-');
                    if (parts.Length == 1 || parts[1] != model.Version)
                    {
                        // The cookie version does not match, so it needs 
                        // to be refreshed.
                        Request.Cookies.Remove("profile");
                    }
                }
                else
                {
                    // The cookie does not contain an ID, so it needs to be
                    // refreshed.
                    Request.Cookies.Remove("profile");
                }
            }

            Response.ContentType = "text/javascript";

            return PartialView("Profile.js", model);
        }

        /// <summary>
        /// Tries to get the profile from the cache before parsing the file on disk.
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private ProfileManifest GetProfile()
        {
            var model = (this.HttpContext.Cache != null) ? this.HttpContext.Cache["profile"] as ProfileManifest : null;
            if (model == null)
            {
                lock (ProfileLock)
                {
                    model = (this.HttpContext.Cache != null) ? HttpContext.Cache["profile"] as ProfileManifest : null;
                    if (model == null)
                    {
                        model = this._profileRepository.GetProfile("generic");
                        if (HttpContext.Cache != null)
                        {
                            HttpContext.Cache.Add("profile",
                                model,
                                new CacheDependency(this._profileRepository.GetManifestPath("generic")),
                                Cache.NoAbsoluteExpiration,
                                TimeSpan.FromHours(1),
                                CacheItemPriority.Normal,
                                null);
                        }
                    }
                }
            }

            return model;
        }
    }
}