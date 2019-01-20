using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TouristWebSite.Startup))]
namespace TouristWebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
