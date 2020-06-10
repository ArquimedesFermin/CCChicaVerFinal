using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CChica.Startup))]
namespace CChica
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
