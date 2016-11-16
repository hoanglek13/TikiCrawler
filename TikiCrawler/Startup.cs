using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TikiCrawler.Startup))]
namespace TikiCrawler
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
