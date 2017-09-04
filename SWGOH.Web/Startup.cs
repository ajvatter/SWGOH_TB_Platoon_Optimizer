using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SWGOH.Web.Startup))]
namespace SWGOH.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
