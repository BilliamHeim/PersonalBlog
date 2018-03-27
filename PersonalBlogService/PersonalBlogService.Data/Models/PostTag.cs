namespace PersonalBlogService.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PostTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PostId { get; set; }

        public int? TagId { get; set; }

        public virtual Post Post { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
