using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lolo.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        [Required]
        public IdentityUser User { get; set; }

        public virtual Post Post { get; set; }
        [Required]
        [StringLength(1000)]
        public string Content { get; set; }
    }
}
