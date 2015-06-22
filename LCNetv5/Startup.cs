using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LCNetv5.Startup))]
namespace LCNetv5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
