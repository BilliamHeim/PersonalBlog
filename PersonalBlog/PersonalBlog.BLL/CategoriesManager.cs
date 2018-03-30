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
    public class CategoriesManager
    {
        private CategoriesRepo repo = new CategoriesRepo();

        public CategoryResponse Add(Category category)
        {
            var context = new PersonalBlogEntities();
            
            if(context.Categories.Contains(category))
            {
                var response = new CategoryResponse();
                response.Success = false;
                response.Message = $"The category {category.CategoryName} is already in the database.";
                return response;
            }
            else
            {
                return repo.Add(category);
            }
        }

        public CategoryResponse GetAll()
        {
            return repo.GetAll();
        }

        public CategoryResponse GetById(int id)
        {
            var context = new PersonalBlogEntities();

            if (id == 0)
            {
                return repo.GetById(id);
            }

            if (context.Categories.FirstOrDefault(c => c.CategoryId == id) == null)
            {
                var response = new CategoryResponse();
                response.Success = false;
                response.Message = "That category is not valid.";
                return response;
            }

            return repo.GetById(id);
        }

        public CategoryResponse Edit(Category category)
        {
            var context = new PersonalBlogEntities();

            if (context.Categories.Contains(category))
            {
                var response = new CategoryResponse();
                response.Success = false;
                response.Message = $"The category {category.CategoryName} is already in the database.";
                return response;
            }
            else
            {
                return repo.Edit(category);
            }
        }

        public CategoryResponse Delete(int id)
        {
            var context = new PersonalBlogEntities();

            if (id == 0)
            {
                return repo.GetById(id);
            }

            if (context.Categories.FirstOrDefault(c => c.CategoryId == id) == null)
            {
                var response = new CategoryResponse();
                response.Success = false;
                response.Message = "There is no category in our database that matches the delete criteria.";
                return response;
            }

            return repo.Delete(id);
        }
    }
}
