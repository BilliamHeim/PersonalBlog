using PersonalBlog.BLL;
using PersonalBlog.Models;
using PersonalBlog.UI.Models;
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
		[System.Web.Http.Route("search/{Categories}/{tag}")]
		[System.Web.Http.AcceptVerbs("GET")]
		public IHttpActionResult Search(string Categories, string tag)
		{
			string cat = Categories;
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
				foreach (var post in response)
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
				var response = tagManager.GetByName('#' + tag);
				if (response.Success == false)
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
				PostsManager postManager = new PostsManager();
				TagsManager tagManager = new TagsManager();
				var postResponseCat = postManager.GetByCategory(cat);
				var tagResponse = tagManager.GetByName("#" + tag);
				if (tagResponse.Success == false && postResponseCat.Success == false)
				{
					return NotFound();
				}
				else if(tagResponse.Success == true && postResponseCat.Success == false)
				{
					var allPosts = postManager.GetByTag(tagResponse.Tags.First().TagId);
					if (allPosts.Success)
					{
						List<PostWebAPI> returnObj = new List<PostWebAPI>();
						foreach(var post in allPosts.Posts)
						{
							PostWebAPI current = new PostWebAPI();
							current.PostId = post.PostId;
							current.PostTitle = post.PostTitle;
							current.PostBody = post.PostBody;
							returnObj.Add(current);
						}
						return Ok(returnObj);
					}
					else
					{
						return NotFound();
					}
				}
				else if(tagResponse.Success == false && postResponseCat.Success == true)
				{
					var allPosts = postManager.GetByCategory(cat);
					if(allPosts.Success == true)
					{
						List<PostWebAPI> returnObj = new List<PostWebAPI>();
						foreach (var post in allPosts.Posts)
						{
							PostWebAPI current = new PostWebAPI();
							current.PostId = post.PostId;
							current.PostTitle = post.PostTitle;
							current.PostBody = post.PostBody;
							returnObj.Add(current);
						}
						return Ok(returnObj);
					}
					else
					{
						return NotFound();
					}
				}
				else
				{
					var allPostsTag = postManager.GetByTag(tagResponse.Tags.First().TagId);
					var allPostsCat = postManager.GetByCategory(cat);
					List<PostWebAPI> returnObj = new List<PostWebAPI>();
					foreach(var post in allPostsTag.Posts)
					{
						PostWebAPI current = new PostWebAPI();
						current.PostId = post.PostId;
						current.PostTitle = post.PostTitle;
						current.PostBody = post.PostBody;
						returnObj.Add(current);
					}
					foreach (var post in allPostsCat.Posts)
					{
						PostWebAPI current = new PostWebAPI();
						current.PostId = post.PostId;
						current.PostTitle = post.PostTitle;
						current.PostBody = post.PostBody;
						returnObj.Add(current);
					}
					var postsOrdered = returnObj.OrderBy(m => m.PostId).ToList();
					int index = 0;
					while(index < postsOrdered.Count() - 1)
					{
						if(postsOrdered[index].PostId == postsOrdered[index + 1].PostId)
						{
							postsOrdered.Remove(postsOrdered[index]);
						}
						index += 1;
					}
					return Ok(postsOrdered);
				}
			}
		}
	}
}