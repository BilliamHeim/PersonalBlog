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
            PostsManager postManager = new PostsManager();
            var response = postManager.GetAll();
			return View();
		}

		public ActionResult Search()
		{
			return View();
		}
	}
}