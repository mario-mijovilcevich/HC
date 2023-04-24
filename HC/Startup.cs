using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HC.Startup))]
namespace HC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
