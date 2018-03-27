using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Models.Models
{
	public class Posts
	{
		[Key]
		public int PostId { get; set; }
		public string PostTitle { get; set; }
		public string PostBody { get; set; }
		public bool IsApproved { get; set; }
		public DateTime CreatedDate { get; set; }
		public int CategoryId { get; set; }

        public virtual ICollection<Tags> Tags { get; set; }
	}
}
