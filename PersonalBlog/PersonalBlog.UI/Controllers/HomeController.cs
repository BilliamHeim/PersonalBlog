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
			var response = postManager.GetAll();
			var model = response.Posts;
			return View(model);
		}

		public ActionResult Search()
		{
			return View();
		}
	}
}