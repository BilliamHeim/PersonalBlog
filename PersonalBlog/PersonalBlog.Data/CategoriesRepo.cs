using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalBlog.Models.Reponses;

namespace PersonalBlog.Data
{
    public class CategoriesRepo
    {
        public CategoryResponse GetAll()
        {
            CategoryResponse response = new CategoryResponse();

            using (var context = new PersonalBlogEntities())
            {
                try
                {
                    response.Categories = context.Categories.ToList();
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

        public CategoryResponse GetById(int id)
        {
            CategoryResponse response = new CategoryResponse();

            if (id == 0)
            {
                response.Success = false;
                response.Message = "Value for Id not passed in.";
                return response;
            }

            using (var context = new PersonalBlogEntities())
            {
                try
                {
                    response.Categories = context.Categories
                        .Where(c => c.CategoryId == id)
                        .ToList();
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
}
