using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lolo.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "FieldRequired")]
        public string Title { get; set; }
        public List<Post> Posts { get; set; }
        public IdentityUser User { get; set; }
    }
}
