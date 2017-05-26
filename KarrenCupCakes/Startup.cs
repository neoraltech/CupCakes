using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KarrenCupCakes.Startup))]
namespace KarrenCupCakes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

    }
}