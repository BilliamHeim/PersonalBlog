using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Movie_Catalog.Models.Identity;
using PersonalBlog.BLL;
using PersonalBlog.Models.Tables;
using PersonalBlog.UI.Models;
using PersonalBlog.UI.Models.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
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

				return RedirectToAction("Panel", "Admin");
			}
		}

		[HttpGet]
		[Authorize(Roles = "Admin, Marketing")]
		public ActionResult Panel()
		{
			return View();
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ActionResult Accounts()
		{
			var context = new PersonalBlogDbContext();
			var marketingRole = (from r in context.Roles where r.Name.Contains("Marketing") select r).FirstOrDefault();
			var marketingUsers = context.Users.Where(u => u.Roles.Select(r => r.RoleId).Contains(marketingRole.Id));
			var adminVM = marketingUsers.Select(user => new UserVM
			{
				Id = user.Id,
				UserName = user.UserName,
				Role = "Marketing"
			}).ToList();
			return View(adminVM);
		}

		[HttpGet]
		[Authorize(Roles = "Admin, Marketing")]
		public ActionResult Add()
		{
			var model = new CreateUserVM();
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Admin, Marketing")]
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

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public ActionResult Delete(string id)
		{
			var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
			var context = new PersonalBlogDbContext();
			//var adminRole = (from r in context.Roles where r.Name.Contains("Admin") select r).FirstOrDefault();
			//var adminUsers = context.Users.Where(u => u.Roles.Select(r => r.RoleId).Contains(adminRole.Id));
			AppUser userToDelete = userManager.FindById(id);
			userManager.Delete(userToDelete);
			return RedirectToAction("Accounts", "Admin");
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ActionResult Edit(string id)
		{
			var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
			var userToEdit = userManager.FindById(id);
			var model = userToEdit;
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public ActionResult Edit(AppUser recievedUser, string id, string password)
		{
			var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
			var oldUser = userManager.FindById(id);
			if (!string.IsNullOrEmpty(password))
			{
				PasswordHasher passwordHasher = new PasswordHasher();
				oldUser.PasswordHash = passwordHasher.HashPassword(password);
				oldUser.UserName = recievedUser.UserName;
				userManager.Update(oldUser);
			}
			else
			{
				oldUser.UserName = recievedUser.UserName;
				userManager.Update(oldUser);
			}
			return RedirectToAction("Accounts", "Admin");
		}

		[HttpGet]
		[Authorize(Roles = "Admin, Marketing")]
		public ActionResult AddPost()
		{
			PostSubmissionVM model = new PostSubmissionVM();
			CategoriesManager manager = new CategoriesManager();
			var allCategories = manager.GetAll();
			foreach (var cat in allCategories.Categories)
			{
				model.Categories.Add(cat);
			}
			return View(model);
		}

		[HttpPost, ValidateInput(false)]
		[Authorize(Roles = "Admin, Marketing")]
		public ActionResult AddPost(PostSubmissionVM post, string Categories)
		{
			Post postSubmit = new Post();
			postSubmit.CategoryId = int.Parse(Categories);
			postSubmit.PostBody = post.Body;
			postSubmit.PostTitle = post.Title;
			PostsManager manager = new PostsManager();

            Post virtaPost = postSubmit;
            virtaPost.PostId = manager.GetAll().Posts.Count() + 1;
            var regex = new Regex(@"(?<=#)\w+");
            var matches = regex.Matches(post.Body);
            foreach (Match m in matches)
            {
                postSubmit.Tags.Add(new Tag { TagName = "#"+m.Value, Posts = new List<Post> { virtaPost } });
            }

            if (User.IsInRole("Admin"))
			{
				postSubmit.IsApproved = true;
			}
			else
			{
				postSubmit.IsApproved = false;
			}
			manager.Add(postSubmit);
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public ActionResult DeletePost(string id)
		{
			PostsManager manager = new PostsManager();
			manager.Delete(int.Parse(id));
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ActionResult EditPost(string id)
		{
			PostsManager manager = new PostsManager();
			var response = manager.GetById(int.Parse(id));
			if (response.Success)
			{
				var model = response.Posts.First();
				return View(model);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		[Authorize(Roles = "Admin")]
		[HttpPost, ValidateInput(false)]
		public ActionResult UpdatePost(string id, string title, string body)
		{
			PostsManager manager = new PostsManager();
			var response = manager.GetById(int.Parse(id));
			var postToUpdate = response.Posts.First();
			postToUpdate.PostTitle = title;
			postToUpdate.PostBody = body;
			manager.Edit(postToUpdate);
			return RedirectToAction("Index", "Home");
		}

		[Authorize(Roles = "Admin, Marketing")]
		[HttpGet]
		public ActionResult Logout()
		{
			var AuthManager = HttpContext.GetOwinContext().Authentication;
			AuthManager.SignOut();
			return RedirectToAction("Index", "Home");
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public ActionResult ApprovePosts()
		{
			PostsManager manager = new PostsManager();
			var response = manager.GetByApproval(false);
			var model = response.Posts;
			return View(model);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public ActionResult ApprovePost(string id)
		{
			PostsManager manager = new PostsManager();
			var response = manager.GetById(int.Parse(id));
			var post = response.Posts.First();
			post.IsApproved = true;
			manager.Edit(post);
			return RedirectToAction("Panel", "Admin");
		}
	}
}