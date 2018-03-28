using PersonalBlog.Data;
using PersonalBlog.Models.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.BLL
{
    public class PostsManager
    {
        private PostsRepo repo = new PostsRepo();

        public PostsResponse GetAll()
        {
            return repo.GetAll();
        }

        public PostsResponse GetByTag(int tagId)
        {
            var context = new PersonalBlogEntities();

            if(tagId == 0)
            {
                return repo.GetByTag(tagId);
            }

            if(context.Tags.FirstOrDefault(t => t.TagId == tagId) == null)
            {
                var response = new PostsResponse();
                response.Success = false;
                response.Message = "That is not a valid tag.";
                return response;
            }

            return repo.GetByTag(tagId);
            
        }

        public PostsResponse GetByApproval(bool approval)
        {
            return repo.GetByApproval(approval);
        }

        public PostsResponse GetByTitle(string title)
        {
            var context = new PersonalBlogEntities();

            if (string.IsNullOrEmpty(title))
            {
                return repo.GetByTitle(title);
            }

            if(context.Posts.FirstOrDefault(p => p.PostTitle == title) == null)
            {
                var response = new PostsResponse();
                response.Success = false;
                response.Message = $"There are no posts that have the name of {title}";
                return response;
            }

            return repo.GetByTitle(title);
        }

        public PostsResponse GetByCategory(string category)
        {
            var context = new PersonalBlogEntities();
            var possibleCategory = context.Categories.FirstOrDefault(c => c.CategoryName.ToLower() == category.ToLower());

            if (possibleCategory == null)
            {
                var response = new PostsResponse();
                response.Success = false;
                response.Message = $"{category} is not a valid category.";
                return response;
            }
            else
            {
                return repo.GetByCategory(possibleCategory.CategoryId);
            }
        }
    }
}
