using PersonalBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalBlog.UI.Models
{
    public class PostSubmissionVM
    {
        string Title { get; set; }
        string Body { get; set; }
        List<Category> Categories { get; set; }
        List<Tag> Tags { get; set; }
    }
}