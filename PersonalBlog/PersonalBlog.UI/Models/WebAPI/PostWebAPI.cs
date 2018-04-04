using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalBlog.UI.Models.WebAPI
{
	public class PostWebAPI
	{
		public int PostId { get; set; }
		public string PostTitle { get; set; }
		public string PostBody { get; set; }
	}
}