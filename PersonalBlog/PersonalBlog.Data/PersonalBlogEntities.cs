using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Data
{
	class PersonalBlogEntities : DbContext
	{
		public PersonalBlogEntities()
		: base("PersonalBlog")
		{
		}
	}
}
