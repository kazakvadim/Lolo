using Lolo.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lolo.Models
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext _appDbContext;

        public BlogRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddBlog(Blog blog)
        {
            _appDbContext.Blogs.Add(blog);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Blog> GetAllBlogs(IdentityUser user)
        {
            return _appDbContext.Blogs.Where(p => p.User == user).ToList();
        }

        public Blog GetBlogById(int blogID)
        {
            return _appDbContext.Blogs.FirstOrDefault(p => p.Id == blogID);
        }

        public Blog GetBlogByUser(IdentityUser user)
        {
            return _appDbContext.Blogs.FirstOrDefault(p => p.User == user);
        }
    }
}
