using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NiyatJobs.Startup))]
namespace NiyatJobs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
