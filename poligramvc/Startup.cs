using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(poligramvc.Startup))]
namespace poligramvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
