using Lolo.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lolo.Models
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _appDbContext;

        public PostRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddPost(Post post)
        {
            _appDbContext.Posts.Add(post);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _appDbContext.Posts;
        }

        public Post GetPostById(int postId)
        {
            return _appDbContext.Posts.FirstOrDefault(p => p.PostId == postId);
        }

        public IEnumerable<Post> GetBlogPosts(Blog blog)
        {
            return _appDbContext.Posts.Where(p => p.Blog == blog).ToList();
        }
    }
}
