using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalBlog.Models.Reponses;
using PersonalBlog.Models.Tables;

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
                    if (response.Categories.Count == 0)
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
                    if (response.Categories.Count == 0)
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

                return response;
            }
        }

        public CategoryResponse Add(Category cat)
        {
            CategoryResponse response = new CategoryResponse();

            if (string.IsNullOrEmpty(cat.CategoryName))
            {
                response.Success = false;
                response.Message = "Category Name cannot be false";
                return response;
            }

            try
            {
                using (var context = new PersonalBlogEntities())
                {
                    context.Categories.Add(cat);
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

        public CategoryResponse Edit(Category cat)
        {
            CategoryResponse response = new CategoryResponse();

            if (cat.CategoryId == 0 || string.IsNullOrEmpty(cat.CategoryName))
            {
                response.Success = false;
                response.Message = "Required field was null";
                return response;
            }

            try
            {
                using (var context= new PersonalBlogEntities())
                {
                    var toEdit = context.Categories.Where(c => c.CategoryId == cat.CategoryId).First();
                    toEdit = cat;
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

        public CategoryResponse Delete(int id)
        {
            CategoryResponse response = new CategoryResponse();

            if (id == 0)
            {
                response.Success = false;
                response.Message = "Category Id was empty/default";
                return response;
            }

            try
            {
                using (var context = new PersonalBlogEntities())
                {
                    var toRemove = context.Categories.Where(c => c.CategoryId == id).First();
                    context.Categories.Remove(toRemove);
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
