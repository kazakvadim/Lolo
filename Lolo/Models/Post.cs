using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lolo.Models
{
    public class Post
    {
        [BindNever]
        public int PostId { get; set; }

        [Required(ErrorMessage = "FieldRequired")]
        [StringLength(40, ErrorMessage = "Title length should be not more than 40 characters!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "FieldRequired")]
        [StringLength(1000, ErrorMessage = "Content length should be not more than 1000 characters!")]
        public string Content { get; set; }

        public DateTime DatePublished { get; set; }
        public string PostUrl { get; set; }
        
        [Required]
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public List<PostTag> Tags { get; } = new List<PostTag>();
        public List<Comment> Comments { get; } = new List<Comment>();
    }
}
