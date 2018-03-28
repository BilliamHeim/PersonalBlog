using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Movie_Catalog.Models.Identity;
using PersonalBlog.UI.Models;
using PersonalBlog.UI.Models.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PersonalBlog.UI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
		[AllowAnonymous]
        public ActionResult Login()
        {
			var model = new LoginViewModel();
            return View(model);
        }

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginViewModel model, string returnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
			var authManager = HttpContext.GetOwinContext().Authentication;

			// attempt to load the user with this password
			AppUser user = userManager.Find(model.UserName, model.Password);

			// user will be null if the password or user name is bad
			if (user == null)
			{
				ModelState.AddModelError("", "Invalid username or password");

				return View(model);
			}
			else
			{
				// successful login, set up their cookies and send them on their way
				var identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
				authManager.SignIn(new AuthenticationProperties { IsPersistent = model.RememberMe }, identity);

				if (!string.IsNullOrEmpty(returnUrl))
					return Redirect(returnUrl);
				else
					return RedirectToAction("Panel", "Admin");
			}
		}

		[HttpGet]
		[Authorize(Roles ="Admin")]
		public ActionResult Panel()
		{
			return View();
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ActionResult Accounts()
		{
			var context = new PersonalBlogDbContext();
			var adminRole = (from r in context.Roles where r.Name.Contains("Admin") select r).FirstOrDefault();
			var adminUsers = context.Users.Where(u => u.Roles.Select(r => r.RoleId).Contains(adminRole.Id));
			var adminVM = adminUsers.Select(user => new UserVM
			{
				Id = user.Id,
				UserName = user.UserName,
				Role = "Admin"
			}).ToList();
			return View(adminVM);
		}

		[HttpGet]
		[Authorize(Roles="Admin")]

		public ActionResult Add()
		{
			var model = new CreateUserVM();
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]

		public ActionResult Add(CreateUserVM model)
		{
			var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
			var user = new AppUser();
			user.UserName = model.UserName;
			var result = userManager.Create(user, model.Password);
			if (result.Succeeded)
			{
				var userAdded = userManager.FindByName(model.UserName);
				result = userManager.AddToRole(userAdded.Id, "Admin");
				if (result.Succeeded)
				{
					return RedirectToAction("Accounts", "Admin");
				}
			}

			return View();
		}
	}
}