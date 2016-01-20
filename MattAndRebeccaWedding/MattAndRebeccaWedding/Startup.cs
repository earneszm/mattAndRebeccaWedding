using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MattAndRebeccaWedding.Startup))]
namespace MattAndRebeccaWedding
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
