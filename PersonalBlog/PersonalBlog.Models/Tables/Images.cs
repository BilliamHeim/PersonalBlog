﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Models.Models
{
	public class Images
	{
		[Key]
		public int ImageId { get; set; }
		public int PostId { get; set; }
		public string ImagePath { get; set; }
	}
}