using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Models.Models
{
	public class PostTags
	{
		[Key]
		public int PostId { get; set; }
		public List<Tags> TagId { get; set; }
	}
}
