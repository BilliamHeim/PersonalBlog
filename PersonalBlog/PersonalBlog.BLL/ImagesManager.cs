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
    public class ImagesManager
    {
        private ImagesRepo repo = new ImagesRepo();

        public ImageResponse GetImage(int id)
        {
            using(var context = new PersonalBlogEntities())
            {
                if(id == 0)
                {
                    return repo.GetImage(id);
                }

                if(context.Images.FirstOrDefault(i => i.ImageId == id) == null)
                {
                    var response = new ImageResponse();
                    response.Success = false;
                    response.Message = "There is no image in the database matching yor search criteria.";
                    return response;
                }

                return repo.GetImage(id);
            }
        }

        public ImageResponse Add(Images image)
        {
            var response = new ImageResponse();

            using(var context = new PersonalBlogEntities())
            {
                if (image.PostId == 0 || string.IsNullOrEmpty(image.ImagePath))
                {
                    return repo.Add(image);
                }

                if(context.Images.FirstOrDefault(i => i.PostId == image.PostId).PostId == image.PostId 
                    && context.Images.FirstOrDefault(i => i.PostId == image.PostId).ImagePath == image.ImagePath)
                {
                    response.Success = false;
                    response.Message = "That image already exists for this post.";
                    return response;
                }

                return repo.Add(image);
            }
        }

        public ImageResponse Edit(Images image)
        {
            var response = new ImageResponse();

            using (var context = new PersonalBlogEntities())
            {
                if (image.PostId == 0 || string.IsNullOrEmpty(image.ImagePath))
                {
                    return repo.Add(image);
                }

                if (context.Images.FirstOrDefault(i => i.PostId == image.PostId).PostId == image.PostId
                    && context.Images.FirstOrDefault(i => i.PostId == image.PostId).ImagePath == image.ImagePath)
                {
                    response.Success = false;
                    response.Message = "That image already exists for this post.";
                    return response;
                }

                return repo.Edit(image);
            }
        }

        public ImageResponse Delete(int id)
        {
            var response = new ImageResponse();

            using (var context = new PersonalBlogEntities())
            {
                if (id == 0)
                {
                    return repo.Delete(id);
                }

                if (context.Images.FirstOrDefault(i => i.ImageId == id) == null)
                {
                    response.Success = false;
                    response.Message = "There's no image in the database with your search criteria to delete.";
                    return response;
                }

                return repo.Delete(id);
            }
        }
    }
}
