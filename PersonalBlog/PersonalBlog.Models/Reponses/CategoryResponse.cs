using PersonalBlog.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Models.Reponses
{
    public class CategoryResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Categories> Categories { get; set; }
    }
}
