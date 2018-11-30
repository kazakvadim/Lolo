using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lolo.Models;
using Lolo.Models.Interfaces;
using Lolo.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lolo.Controllers
{
    [Route("api/[controller]")]
    public class PostDataController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IBlogRepository _blogRepository;
        private UserManager<IdentityUser> _userManager;

        public PostDataController(IPostRepository postRepository, IBlogRepository blogRepository, UserManager<IdentityUser> userManager)
        {
            _postRepository = postRepository;
            _blogRepository = blogRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IEnumerable<PostViewModel>> LoadMorePagesAsync()
        {
            IEnumerable<Post> dbPosts = null;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var blog = _blogRepository.GetBlogByUser(user);
            dbPosts = _postRepository.GetBlogPosts(blog).Take(4);

            List<PostViewModel> posts = new List<PostViewModel>();
            foreach (var dbPost in dbPosts)
            {
                posts.Add(MapDbPostToPostViewModel(dbPost));
            }
            return posts;
           
        }

        private PostViewModel MapDbPostToPostViewModel(Post dbPost)
        {
            return new PostViewModel()
            {
                Id = dbPost.PostId,
                Title = dbPost.Title
            };
        }
    }
}
