﻿using PersonalBlog.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Models.Reponses
{
    public class PostsResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Posts> Posts { get; set; }
    }
}
