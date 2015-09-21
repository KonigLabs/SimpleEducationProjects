using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using testWurflLocalIIS.Models;

namespace testWurflLocalIIS.ClientProfile
{
    public class XmlProfileManifestRepository : IProfileManifestRepository
    {
        readonly Func<string, string> _virtualPathResolver;
        readonly string _virtualPath;

        public XmlProfileManifestRepository(string virtualPath, Func<string, string> virtualPathResolver)
        {
            _virtualPathResolver = virtualPathResolver;
            _virtualPath = virtualPath;
        }

        public string GetManifestPath(string name)
        {
            return _virtualPathResolver(_virtualPath + name + ".xml");
        }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public ProfileManifest GetProfile(string name)
        {
            var filePath = GetManifestPath(name);

            using (var sr = new StreamReader(filePath))
            using (var reader = XmlReader.Create(sr))
            {
                var profile = XmlProfileManifestParser.Parse(reader);
                return profile;
            }
        }
    }
}