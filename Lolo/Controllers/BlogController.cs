using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lolo.Models;
using Lolo.Models.Interfaces;
using Lolo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lolo.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IPostRepository _postRepository;
        private UserManager<IdentityUser> _userManager;

        public BlogController(IBlogRepository blogRepository, IPostRepository postRepository, UserManager<IdentityUser> userManager)
        {
            _postRepository = postRepository;
            _blogRepository = blogRepository;
            _userManager = userManager;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var blogs = _blogRepository.GetAllBlogs(user);
            var blogViewModel = new BlogViewModel()
            {
                Title = "My blogs",
                Blogs = blogs.ToList()
            };
            return View(blogViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var blog = _blogRepository.GetBlogById(id);
            var posts = _postRepository.GetBlogPosts(blog);
            var postViewModel = new PostViewModel()
            {
                Title = "My posts",
                Posts = posts.ToList()
            };
            return View(postViewModel);
        }

        [HttpGet]
        public IActionResult AddBlog()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBlog(Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.User = await _userManager.GetUserAsync(HttpContext.User);
                _blogRepository.AddBlog(blog);

                return RedirectToAction("Index");
            }
            else
            {
                return View(blog);
            }
        }
    }
}
