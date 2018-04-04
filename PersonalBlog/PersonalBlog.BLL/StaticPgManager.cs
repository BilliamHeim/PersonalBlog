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
    public class StaticPgManager
    {
        StaticPgRepo repo = new StaticPgRepo();

        public StaticPgResponse GetById(int pageId)
        {
            StaticPgResponse response = new StaticPgResponse();
            if (pageId > 0 && pageId > 3)
            {
                response.Success = false;
                response.Message = "You messed up the id brah.";
                return response;
            }
            else
            {
                response = repo.GetById(pageId);
            }
            return response;
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
            else
            {
                response = repo.Edit(page);
            }
            return response;
        }
    }
}
