using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Models.Models
{
    public class Tags
    {
        [Key]
        public int TagId { get; set; }
        public string TagName { get; set; }

        public virtual ICollection<Posts> Posts{get;set;}
	}
}
