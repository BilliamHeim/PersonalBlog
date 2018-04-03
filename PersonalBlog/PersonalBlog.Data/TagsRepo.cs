using PersonalBlog.Models.Reponses;
using PersonalBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Data
{
    public class TagsRepo
    {
        public TagsResponse GetAll()
        {
            TagsResponse response = new TagsResponse();

            using (var context=new PersonalBlogEntities())
            {
                try
                {
                    response.Tags = context.Tags
                        .Include("Posts")
                        .ToList();
                    if (response.Tags.Count == 0)
                    {
                        response.Success = false;
                        response.Message = "Nothing found.";
                        return response;
                    }
                    response.Success = true;
                }
                catch(Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                }
            }

            return response;
        }

        public TagsResponse GetByName(string tag)
        {
            TagsResponse response = new TagsResponse();

            if (string.IsNullOrEmpty(tag))
            {
                response.Success = false;
                response.Message = "Tag value empty.";
                return response;
            }

            using (var context = new PersonalBlogEntities())
            {
                try
                {
                    response.Tags = context.Tags
                        .Include("Posts")
                        .Where(t => (t.TagName.ToLower()).Contains(tag.ToLower()))
                        .ToList();
                    if (response.Tags.Count==0)
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

        public TagsResponse GetById(int id)
        {
            TagsResponse response = new TagsResponse();

            if (id == 0)
            {
                response.Success = false;
                response.Message = "TagId value empty.";
            }

            using (var context = new PersonalBlogEntities())
            {
                try
                {
                    response.Tags = context.Tags
                        .Include("Posts")
                        .Where(t=>t.TagId==id)
                        .ToList();
                    if (response.Tags.Count == 0)
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

        public TagsResponse Add(Tag tag)
        {
            TagsResponse response = new TagsResponse();

            if (string.IsNullOrEmpty(tag.TagName) || tag.Posts.Count() < 1)
            {
                response.Success = false;
                response.Message = "Name is required, tag must be associated with at least one post.";
                return response;
            }

            try
            {
                using (var context=new PersonalBlogEntities())
                {
                    context.Tags.Add(tag);
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

        public TagsResponse Edit(Tag tag)
        {
            TagsResponse response = new TagsResponse();

            if (string.IsNullOrEmpty(tag.TagName) || tag.Posts.Count() < 1)
            {
                response.Success = false;
                response.Message = "Name is required, tag must be associated with at least one post.";
                return response;
            }

            try
            {
                using (var context = new PersonalBlogEntities())
                {
                    var toEdit = context.Tags.Where(t => t.TagId == tag.TagId).First();
                    toEdit = tag;
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

        public TagsResponse Delete(int id)
        {
            TagsResponse response = new TagsResponse();

            if (id == 0)
            {
                response.Success = false;
                response.Message = "Tag id was not passed or was null/default.";
                return response;
            }

            try
            {
                using (var context = new PersonalBlogEntities())
                {
                    var toRemove = context.Tags.Where(t => t.TagId == id).First();
                    context.Tags.Remove(toRemove);
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
    }
}
