namespace PersonalBlog.UI.Migrations
{
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using PersonalBlog.UI.Models.Identity;
	using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Movie_Catalog.Models.Identity.PersonalBlogDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Movie_Catalog.Models.Identity.PersonalBlogDbContext context)
        {
			// Load the user and role managers with our custom models
			var userMgr = new UserManager<AppUser>(new UserStore<AppUser>(context));
			var roleMgr = new RoleManager<AppRole>(new RoleStore<AppRole>(context));

			// have we loaded roles already?
			if (roleMgr.RoleExists("Admin"))
				return;

			// create the admin role
			roleMgr.Create(new AppRole() { Name = "Admin" });

			// create the default user
			var user = new AppUser()
			{
				UserName = "admin"
			};

			// create the user with the manager class
			userMgr.Create(user, "testing123");

			// add the user to the admin role
			userMgr.AddToRole(user.Id, "admin");
		}
    }
}
