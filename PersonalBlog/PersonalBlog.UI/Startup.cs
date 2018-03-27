using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PersonalBlog.UI.Startup))]
namespace PersonalBlog.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
