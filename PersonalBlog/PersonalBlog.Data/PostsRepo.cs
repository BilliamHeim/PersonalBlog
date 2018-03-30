using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalBlog.Models.Reponses;
using PersonalBlog.Models.Tables;

namespace PersonalBlog.Data
{
    public class PostsRepo
    {
        public PostsResponse GetAll()
        {
            PostsResponse response = new PostsResponse();

            using (var context = new PersonalBlogEntities())
            {
                try
                {
                    response.Posts = context.Posts
                        .Include("Tags")
                        .ToList();
                    response.Success = true;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                }
            }

            return response;
        }

        public PostsResponse GetById(int id)
        {
            PostsResponse response = new PostsResponse();

            if (id == 0)
            {
                response.Success = false;
                response.Message = "Post ID was invalid.";
                return response;
            }

            using (var context = new PersonalBlogEntities())
            {
                try
                {
                    response.Posts = context.Posts
                        .Include("Tags")
                        .Where(p => p.PostId == id)
                        .ToList();
                    if (response.Posts.Count == 0)
                    {
                        response.Success = false;
                        response.Message = "Nothing found.";
                        return response;
                    }
                    response.Success = true;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                }
            }

            return response;
        }

        public PostsResponse GetByTag(int tagId)
        {
            PostsResponse response = new PostsResponse();

            if (tagId == 0)
            {
                response.Success = false;
                response.Message = "TagId value not passed.";
                return response;
            }

            using (var context = new PersonalBlogEntities())
            {
                try
                {
                    response.Posts = context.Posts
                        .Include("Tags")
                        .Where(p=>p.Tags.Any(t=>t.TagId==tagId))
                        .ToList();
                    if (response.Posts.Count == 0)
                    {
                        response.Success = false;
                        response.Message = "Nothing found.";
                        return response;
                    }
                    response.Success = true;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                }
            }

            return response;
        }

        public PostsResponse GetByApproval(bool isApproved)
        {
            PostsResponse response = new PostsResponse();
            
            using (var context = new PersonalBlogEntities())
            {
                try
                {
                    response.Posts = context.Posts
                        .Include("Tags")
                        .Where(p=>p.IsApproved==isApproved)
                        .ToList();
                    if (response.Posts.Count == 0)
                    {
                        response.Success = false;
                        response.Message = "Nothing found.";
                        return response;
                    }
                    response.Success = true;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                }
            }

            return response;
        }

        public PostsResponse GetByTitle(string title)
        {
            PostsResponse response = new PostsResponse();

            if (string.IsNullOrEmpty(title))
            {
                response.Success = false;
                response.Message = "Value of title missing or empty.";
                return response;
            }

            using (var context = new PersonalBlogEntities())
            {
                try
                {
                    response.Posts = context.Posts
                        .Include("Tags")
                        .Where(p => (p.PostTitle.ToLower()).Contains(title.ToLower()))
                        .ToList();
                    if (response.Posts.Count == 0)
                    {
                        response.Success = false;
                        response.Message = "Nothing found.";
                        return response;
                    }
                    response.Success = true;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                }
            }

            return response;
        }

        public PostsResponse GetByCategory(int catId)
        {
            PostsResponse response = new PostsResponse();

            if (catId == 0)
            {
                response.Success = false;
                response.Message = "CategoryId was invalid.";
                return response;
            }

            using (var context = new PersonalBlogEntities())
            {
                try
                {
                    response.Posts = context.Posts
                        .Include("Tags")
                        .Where(p => p.CategoryId==catId)
                        .ToList();
                    if (response.Posts.Count == 0)
                    {
                        response.Success = false;
                        response.Message = "Nothing found.";
                        return response;
                    }
                    response.Success = true;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                }
            }

            return response;
        }

        public PostsResponse Add(Post post)
        {
            PostsResponse response = new PostsResponse();

            try
            {
                using (var context = new PersonalBlogEntities())
                {
                    context.Posts.Add(post);
                    context.SaveChanges();
                    response.Success = true;
                }
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
        
        public PostsResponse Edit(Post post)
        {
            PostsResponse response = new PostsResponse();

            try
            {
                using (var context=new PersonalBlogEntities())
                {
                    var toEdit = context.Posts.Where(p => p.PostId == post.PostId).First();
                    toEdit = post;
                    context.SaveChanges();
                    response.Success = true;
                }
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public PostsResponse Delete(int postId)
        {
            PostsResponse response = new PostsResponse();

            try
            {
                using (var context = new PersonalBlogEntities())
                {
                    var toDelete = context.Posts.Where(p => p.PostId == postId).First();
                    context.Posts.Remove(toDelete);
                    context.SaveChanges();
                    response.Success = true;
                }
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
