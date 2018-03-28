using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalBlog.Models.Models;
using PersonalBlog.Models.Reponses;

namespace PersonalBlog.Data
{
    public class ImagesRepo
    {
        public ImageResponse GetImage(int id)
        {
            ImageResponse response = new ImageResponse();

            if(id == 0)
            {
                response.Success = false;
                response.Message = "Id value passed was empty/default";
                return response;
            }

            try
            {
                using (var context=new PersonalBlogEntities())
                {
                    response.Images.Add(context.Images.Where(i => i.ImageId == id).First());
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

        public ImageResponse Add(Images image)
        {
            ImageResponse response = new ImageResponse();

            if (image.PostId == 0 || string.IsNullOrEmpty(image.ImagePath))
            {
                response.Success = false;
                response.Message = "Image data or path was missing.";
                return response;
            }

            try
            {
                using (var context = new PersonalBlogEntities())
                {
                    context.Images.Add(image);
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

        public ImageResponse Edit(Images image)
        {
            ImageResponse response = new ImageResponse();

            if (image.PostId == 0 || string.IsNullOrEmpty(image.ImagePath))
            {
                response.Success = false;
                response.Message = "Image data or path was missing.";
                return response;
            }

            try
            {
                using (var context = new PersonalBlogEntities())
                {
                    var toEdit = context.Images.Where(i => i.ImageId == image.ImageId).First();
                    toEdit = image;
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

        public ImageResponse Delete(int id)
        {
            ImageResponse response = new ImageResponse();

            if (id == 0)
            {
                response.Success = false;
                response.Message = "Image id was invalid/empty.";
                return response;
            }

            try
            {
                using (var context=new PersonalBlogEntities())
                {
                    var toRemove = context.Images.Where(i => i.ImageId == id).First();
                    context.Images.Remove(toRemove);
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
