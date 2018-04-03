using PersonalBlog.Data;
using PersonalBlog.Models.Reponses;
using PersonalBlog.Models.Tables;
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

        public PostsResponse GetById(int id)
        {
            PostsResponse response = new PostsResponse();
            if (id == 0)
            {
                response.Success = false;
                response.Message = "Id value was not passed in.";
                return response;
            }
            try
            {
                response = repo.GetById(id);
                if (response.Posts.Count == 0 || response.Posts.First() == null)
                {
                    response.Success = false;
                    response.Message = "No posts found";
                }
                response.Success = true;
            }
            catch(Exception ex)
            {
                response.Success = true;
                response.Message = ex.Message;
            }
            return response;

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

        public PostsResponse Add(Post post)
        {
            var context = new PersonalBlogEntities();
            var response = new PostsResponse();

            if (string.IsNullOrEmpty(post.PostTitle))
            {
                response.Success = false;
                response.Message = "The post title cannot be left blank.";
            }
            else if(post.CreatedDate < DateTime.Today.AddDays(1))
            {
                response.Success = false;
                response.Message = "The post body cannot be left blank";
            }
            //else if (!post.IsApproved)
            //{
            //    response.Success = false;
            //    response.Message = "This post has content that violates our blogging policy.";
            //}
            else if (string.IsNullOrEmpty(post.PostBody))
            {
                response.Success = false;
                response.Message = "The post body cannot be left blank.";
            }
            else if(context.Categories.FirstOrDefault(c => c.CategoryId == post.CategoryId) == null)
            {
                response.Success = false;
                response.Message = "That category is invalid";
            }
            else
            {
                TagsRepo tagsRepo = new TagsRepo();

                List<Tag> allTags = tagsRepo.GetAll().Tags.ToList();
                List<Tag> tagsToAdd = post.Tags.AsEnumerable().Where(t => post.Tags.Any(postTag => postTag.TagName != t.TagName)).ToList();
                foreach (Tag t in tagsToAdd)
                {
                    tagsRepo.Add(t);
                }
                response = repo.Add(post);
                response.Message = $"The post \"{post.PostTitle}\" has been added to the database.";
            }

            return response;
        }

        public PostsResponse Edit(Post post)
        {
            var context = new PersonalBlogEntities();
            var response = new PostsResponse();

            if (string.IsNullOrEmpty(post.PostTitle))
            {
                response.Success = false;
                response.Message = "The post title cannot be left blank.";
            }
            else if (post.CreatedDate < DateTime.Today.AddDays(1))
            {
                response.Success = false;
                response.Message = "The post body cannot be left blank";
            }
            else if (!post.IsApproved)
            {
                response.Success = false;
                response.Message = "This post has content that violates our blogging policy.";
            }
            else if (string.IsNullOrEmpty(post.PostBody))
            {
                response.Success = false;
                response.Message = "The post body cannot be left blank.";
            }
            else if (context.Categories.FirstOrDefault(c => c.CategoryId == post.CategoryId) == null)
            {
                response.Success = false;
                response.Message = "That category is invalid";
            }
            else
            {
                response = repo.Edit(post);
                response.Message = $"Your changes to \"{post.PostTitle}\" have been saved.";
                response.Posts.Add(post);
            }

            return response;
        }

        public PostsResponse Delete(int id)
        {
            var context = new PersonalBlogEntities();

            if(context.Posts.FirstOrDefault(p => p.PostId == id) == null)
            {
                var response = new PostsResponse();
                response.Success = false;
                response.Message = "There is no post in our database that matches the delete criteria.";
                return response;
            }

            return repo.Delete(id);
        }
    }
}
