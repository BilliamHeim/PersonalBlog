using PersonalBlog.Data;
using PersonalBlog.Models.Reponses;
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
                response.Message = "That tag does not have a valid Tag ID.";
                return response;
            }

            return repo.GetById(id);
        }
    }
}
