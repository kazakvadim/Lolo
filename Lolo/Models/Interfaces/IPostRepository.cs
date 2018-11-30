using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lolo.Models.Interfaces
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAllPosts();
        IEnumerable<Post> GetBlogPosts(Blog blog);
        Post GetPostById(int postID);
        void AddPost(Post post);
    }
}
