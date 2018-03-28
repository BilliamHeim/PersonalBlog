using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PersonalBlog.Models.Models
{
	public class Images
	{
		[Key]
		public int ImageId { get; set; }
		public int PostId { get; set; }
		public string ImagePath { get; set; }
        public byte[] Picture { get; set; }
	}
}
