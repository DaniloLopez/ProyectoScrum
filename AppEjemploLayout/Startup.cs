using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppEjemploLayout.Startup))]
namespace AppEjemploLayout
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
