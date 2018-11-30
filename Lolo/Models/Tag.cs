using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lolo.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        [Required(ErrorMessage = "FieldRequired")]
        [StringLength(15, ErrorMessage = "Title length should be not more than 15 characters!")]
        public string Title { get; set; }
        public List<PostTag> PostTags { get; } = new List<PostTag>();
        //public IdentityUser User { get; set; }
        //[Required]
        //public int PostId { get; set; }
        //public Post Post { get; set; }
    }
}
