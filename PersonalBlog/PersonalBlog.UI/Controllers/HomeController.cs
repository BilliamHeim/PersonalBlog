using PersonalBlog.BLL;
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
			return View();
		}
	}
}