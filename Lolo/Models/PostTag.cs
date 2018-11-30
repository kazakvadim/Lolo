using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lolo.Models
{
    public class PostTag
    {
        public int PostId { get; set; }
        public Post Post { get; set; } 

        [Required]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
