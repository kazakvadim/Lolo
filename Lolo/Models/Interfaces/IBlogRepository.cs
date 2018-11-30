using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lolo.Models.Interfaces
{
    public interface IBlogRepository
    {
        IEnumerable<Blog> GetAllBlogs(IdentityUser user);
        Blog GetBlogById(int blogID);
        Blog GetBlogByUser(IdentityUser user);
        void AddBlog(Blog blog);
    }
}
