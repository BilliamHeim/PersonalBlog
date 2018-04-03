using PersonalBlog.BLL;
using PersonalBlog.Models;
using PersonalBlog.UI.Models.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace PersonalBlog.UI.Controllers
{
	public class SearchController : ApiController
	{
		[System.Web.Http.Route("search/{cat}/{tag}")]
		[System.Web.Http.AcceptVerbs("GET")]
		public IHttpActionResult Search(string cat, string tag)
		{
			PostsManager postsManager = new PostsManager();
			if (string.IsNullOrEmpty(cat) && string.IsNullOrEmpty(tag))
			{
				throw new NotImplementedException();
			}

			else if (tag == "emptytag")
			{
				PostsManager postManager = new PostsManager();
				var response = postManager.GetByCategory(cat).Posts.Where(p => p.IsApproved == true);
				List<PostWebAPI> returnObject = new List<PostWebAPI>();
				foreach(var post in response)
				{
					PostWebAPI current = new PostWebAPI();
					current.PostId = post.PostId;
					current.PostBody = post.PostBody;
					current.PostTitle = post.PostTitle;
					returnObject.Add(current);
				}
				return Ok(returnObject);
			}

			else if (cat == "emptytag")
			{
				PostsManager postManager = new PostsManager();
				TagsManager tagManager = new TagsManager();
				var response = tagManager.GetByName(tag);
				if(response.Success == false)
				{
					return NotFound();
				}
				int tagId = response.Tags.First().TagId;
				var responsePosts = postManager.GetByTag(tagId);
				List<PostWebAPI> returnObject = new List<PostWebAPI>();
				foreach (var post in responsePosts.Posts)
				{
					PostWebAPI current = new PostWebAPI();
					current.PostId = post.PostId;
					current.PostBody = post.PostBody;
					current.PostTitle = post.PostTitle;
					returnObject.Add(current);
				}
				return Ok(returnObject);
			}

			else
			{

			}
			
			throw new NotImplementedException();
		}
	}
}