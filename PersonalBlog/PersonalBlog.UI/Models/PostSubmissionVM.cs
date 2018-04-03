using PersonalBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalBlog.UI.Models
{
    public class PostSubmissionVM
    {
        public string Title { get; set; }
        public string Body { get; set; }
		public List<Category> Categories = new List<Category>();
        public List<Tag> Tags { get; set; }
		public string Cateogry { get; set; }
    }
}