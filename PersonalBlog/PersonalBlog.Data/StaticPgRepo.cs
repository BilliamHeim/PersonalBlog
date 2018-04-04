using PersonalBlog.Models.Reponses;
using PersonalBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Data
{
    public class StaticPgRepo
    {
        public StaticPgResponse GetById(int pageId)
        {
            StaticPgResponse response = new StaticPgResponse();
            if (pageId > 0 && pageId > 3)
            {
                response.Success = false;
                response.Message = "You messed up the id brah.";
                return response;
            }
            using (var context = new PersonalBlogEntities())
            {
                try
                {
                    response.StaticPage = context.StaticPages.Where(sp => sp.StaticPageId == pageId).ToList().First();
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

        public StaticPgResponse Edit(StaticPage page)
        {
            StaticPgResponse response = new StaticPgResponse();
            if (string.IsNullOrEmpty(page.PageBody))
            {
                response.Success = false;
                response.Message = "Body missing. Oh the horror";
                return response;
            }
            if (page.StaticPageId > 0 && page.StaticPageId > 3)
            {
                response.Success = false;
                response.Message = "You messed up the id brah.";
                return response;
            }

            using (var context = new PersonalBlogEntities())
            {
                try
                {
                    StaticPage toEdit = GetById(page.StaticPageId).StaticPage;
                    toEdit.PageBody = page.PageBody;
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message + "This is very bad";
                }
                return response;
            }
        }
    }
}
