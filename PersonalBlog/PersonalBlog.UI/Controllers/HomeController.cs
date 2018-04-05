using PersonalBlog.BLL;
using PersonalBlog.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalBlog.UI.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			BLL.PostsManager postManager = new BLL.PostsManager();
			var response = postManager.GetByApproval(true);
			var model = response.Posts;
			foreach(var post in model)
			{
				if(post.PostBody.Length > 200)
				{
					post.PostBody = post.PostBody.Substring(0, 200);
				}
			}
			return View(model);
		}

		public ActionResult Post(string id)
		{
			PostsManager manager = new PostsManager();
			var response = manager.GetById(int.Parse(id));
			var model = response.Posts.First();
			return View(model);
		}

		public ActionResult Search()
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

		public ActionResult Disclaimer()
		{
			StaticPgManager manager = new StaticPgManager();
			var tempmodel = manager.GetById(1);
			var model = tempmodel.StaticPage;
			return View(model);
		}

		public ActionResult About()
		{
			StaticPgManager manager = new StaticPgManager();
			var tempmodel = manager.GetById(2);
			var model = tempmodel.StaticPage;
			return View(model);
		}
	}
}