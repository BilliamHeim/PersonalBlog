namespace PersonalBlogService.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Image
    {
        public int ImageId { get; set; }

        public int? PostId { get; set; }

        [Required]
        [StringLength(300)]
        public string ImagePath { get; set; }

        public virtual Post Post { get; set; }
    }
}
