using PersonalBlog.Data;
using PersonalBlog.Models.Models;
using PersonalBlog.Models.Reponses;
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

        public string AddCategory(string category)
        {
            var response = new CategoryResponse();
            
            if(string.IsNullOrEmpty(category))
            {
                response.Success = false;
                response.Message = "Please enter a category name";
            }
            else
            {
                response.Success = true;
                response.Message = $"The category {category} has been saved to the database.";
                response.Categories.Add(new Categories() { CategoryName = category });
            }

            return response.Message;
        }

        public CategoryResponse GetAll()
        {
            return repo.GetAll();
        }
    }
}
