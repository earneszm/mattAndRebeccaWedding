using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestingAppServices.Startup))]
namespace TestingAppServices
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
