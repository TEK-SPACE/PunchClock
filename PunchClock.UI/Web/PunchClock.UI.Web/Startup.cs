using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PunchClock.UI.Web.Startup))]
namespace PunchClock.UI.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
