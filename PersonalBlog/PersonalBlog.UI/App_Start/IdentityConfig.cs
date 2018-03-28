using Movie_Catalog.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using PersonalBlog.UI.Models.Identity;

namespace Movie_Catalog.App_Start
{
	public class IdentityConfig
	{
		public void Configuration(IAppBuilder app)
		{
			app.CreatePerOwinContext(() => new PersonalBlogDbContext());

			app.CreatePerOwinContext<UserManager<AppUser>>((options, context) =>
				new UserManager<AppUser>(
					new UserStore<AppUser>(context.Get<PersonalBlogDbContext>())));

			app.CreatePerOwinContext<RoleManager<AppRole>>((options, context) =>
				new RoleManager<AppRole>(
					new RoleStore<AppRole>(context.Get<PersonalBlogDbContext>())));

			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString("/Admin/Login"),
			});
		}
	}
}