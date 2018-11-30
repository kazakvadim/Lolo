using Lolo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lolo.ViewModels
{
    public class TagViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<PostTag> Tags { get; set; }
    }
}
