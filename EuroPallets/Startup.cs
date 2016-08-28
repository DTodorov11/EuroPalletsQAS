using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EuroPallets.Startup))]
namespace EuroPallets
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
