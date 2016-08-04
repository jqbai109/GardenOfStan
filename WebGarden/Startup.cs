using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebGarden.Startup))]
namespace WebGarden
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
