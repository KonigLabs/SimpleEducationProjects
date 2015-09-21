using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(testWurflLocalIIS.Startup))]
namespace testWurflLocalIIS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
