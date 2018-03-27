using PersonalBlog.Models.Reponses;
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
                        .Where(t => t.TagName == tag)
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
    }
}
