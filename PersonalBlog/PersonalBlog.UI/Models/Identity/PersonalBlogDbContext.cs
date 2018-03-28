using Microsoft.AspNet.Identity.EntityFramework;
using PersonalBlog.UI.Models.Identity;

namespace Movie_Catalog.Models.Identity
{
	public class PersonalBlogDbContext : IdentityDbContext<AppUser>
	{
		public PersonalBlogDbContext() : base("DefaultConnection")
		{

		}
	}
}