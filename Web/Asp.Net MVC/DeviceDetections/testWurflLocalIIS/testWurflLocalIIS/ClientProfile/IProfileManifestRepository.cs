using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testWurflLocalIIS.Models;

namespace testWurflLocalIIS.ClientProfile
{
    public interface IProfileManifestRepository
    {
        string GetManifestPath(string name);

        ProfileManifest GetProfile(string name);
    }
}