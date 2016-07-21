using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ManageShop.Startup))]
namespace ManageShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
