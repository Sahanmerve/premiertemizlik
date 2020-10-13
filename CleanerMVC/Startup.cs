using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CleanerMVC.Startup))]
namespace CleanerMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
