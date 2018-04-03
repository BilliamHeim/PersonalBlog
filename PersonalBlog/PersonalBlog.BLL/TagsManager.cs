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
    public class TagsManager
    {
        private TagsRepo repo = new TagsRepo();

        public TagsResponse GetAll()
        {
            return repo.GetAll();
        }

        public TagsResponse GetByName(string tag)
        {
            var context = new PersonalBlogEntities();

            if (string.IsNullOrEmpty(tag))
            {
                return repo.GetByName(tag);
            }

            if(context.Tags.FirstOrDefault(p => p.TagName == tag) == null)
            {
                var response = new TagsResponse();
                response.Success = false;
                response.Message = $"There are no posts with the tag {tag}.";
                return response;
            }
            else
            {
                return repo.GetByName(tag);
            }
        }

        public TagsResponse GetById(int id)
        {
            var context = new PersonalBlogEntities();

            if(id == 0)
            {
                return repo.GetById(id);
            }

            if(context.Tags.FirstOrDefault(t => t.TagId == id) == null)
            {
                var response = new TagsResponse();
                response.Success = false;
                response.Message = "That tag is not valid.";
                return response;
            }

            return repo.GetById(id);
        }

        public TagsResponse Add(Tag tag)
        {
            var context = new PersonalBlogEntities();

            if (context.Tags.Contains(tag))
            {
                var response = new TagsResponse();
                response.Success = false;
                response.Message = $"The tag {tag.TagName} already exists in the database.";
                response.Tags.Add(tag);
                return response;
            }
            else
            {
                return repo.Add(tag);
            }
        }

        public TagsResponse Edit(Tag tag)
        {
            var context = new PersonalBlogEntities();

            if (context.Tags.Contains(tag))
            {
                var response = new TagsResponse();
                response.Success = false;
                response.Message = $"The tag {tag} already exists in the database.";
                response.Tags.Add(tag);
                return response;
            }
            else
            {
                return repo.Edit(tag);
            }
        }

        public TagsResponse Delete(int id)
        {
            var context = new PersonalBlogEntities();

            if (context.Tags.FirstOrDefault(t => t.TagId == id) == null)
            {
                var response = new TagsResponse();
                response.Success = false;
                response.Message = "There is no tag in our database that matches the delete criteria.";
                return response;
            }

            return repo.Delete(id);
        }
    }
}
