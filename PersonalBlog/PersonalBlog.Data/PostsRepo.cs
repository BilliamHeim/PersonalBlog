using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
                        .Where(p => p.Tags.Any(t => t.TagId == tagId))
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
                        .Where(p => p.IsApproved == isApproved)
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
                        .Where(p => p.CategoryId == catId)
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
            catch (Exception ex)
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
                using (var context = new PersonalBlogEntities())
                {
                    var toEdit = context.Posts.Where(p => p.PostId == post.PostId).First();

                    context.Entry(toEdit).State = System.Data.Entity.EntityState.Modified;
                    toEdit.CreatedDate = post.CreatedDate;
                    toEdit.ImageFileName = post.ImageFileName;
                    toEdit.IsApproved = post.IsApproved;
                    toEdit.CategoryId = post.CategoryId;
                    toEdit.PostBody = post.PostBody;
                    toEdit.Tags.Clear();
                    toEdit.PostTitle = post.PostTitle;

                    context.SaveChanges();

                    toEdit.Tags = context.Tags.AsEnumerable().Where(t => post.Tags.Any(postTag => postTag.TagId == t.TagId)).ToList();

                    context.SaveChanges();
                    response.Success = true;
                }
            }
            catch (Exception ex)
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
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "DeletePost";
                    cmd.Parameters.AddWithValue("@PostId", postId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
