using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using testWurflLocalIIS.Models;

namespace testWurflLocalIIS.ClientProfile
{
    public static class XmlProfileManifestParser
    {
        public static ProfileManifest Parse(XmlReader reader)
        {
            var root = XElement.Load(reader);

            var profiles = root.DescendantsAndSelf("profile")
                .Select(profile => new ProfileManifest
                {
                    Id = profile.Attribute("id").Value,
                    Title = profile.Attribute("title").Value,
                    Version = profile.Attribute("version").Value,
                    Features = profile.Descendants("feature")
                        .Select(feature => new Feature
                        {
                            Id = feature.Attribute("id").Value,
                            Name = feature.Element("name").Value,
                            Value = feature.Attribute("default").Value,
                            Property = feature.Attribute("property").Value,
                            Test = (feature.Element("test") != null) ? feature.Element("test").Value : null
                        }).ToArray()
                });

            return profiles.FirstOrDefault();
        }
    }
}