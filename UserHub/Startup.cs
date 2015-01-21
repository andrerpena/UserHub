using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UserHub.Startup))]
namespace UserHub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
