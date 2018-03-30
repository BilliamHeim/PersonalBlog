using PersonalBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Models.Reponses
{
    public class TagsResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
