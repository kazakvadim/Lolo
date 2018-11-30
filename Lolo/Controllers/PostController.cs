using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Lolo.Hubs;
using Lolo.Models;
using Lolo.Models.Interfaces;
using Lolo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lolo.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private readonly IPostRepository _postRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly ITagRepository _tagRepository;
        private UserManager<IdentityUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IHubContext<CommentHub> _hubContext;

        public PostController(
            IPostRepository postRepository, 
            IBlogRepository blogRepository, 
            ITagRepository tagRepository, 
            UserManager<IdentityUser> userManager,
            AppDbContext context, 
            IHostingEnvironment environment,
            IHubContext<CommentHub> hubContext)
        {
            _postRepository = postRepository;
            _blogRepository = blogRepository;
            _tagRepository = tagRepository;
            _userManager = userManager;
            _context = context;
            _environment = environment;
            _hubContext = hubContext;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var blog = _blogRepository.GetBlogByUser(user);
            var posts = _postRepository.GetBlogPosts(blog);
            var postViewModel = new PostViewModel()
            {
                Title = "My posts",
                Posts = posts.ToList()
            };
            return View(postViewModel);
        }

        public IActionResult Details(int id)
        {
            var _post = _context.Posts
                .Include(p => p.Blog)
                .ThenInclude(q => q.User)
                .FirstOrDefault(p => p.PostId == id);
            var _blog = _post.Blog;
            var user = _blog.User;
            ViewBag.CurrentUser = user.UserName;
            var post = _context.Posts
                .Include(p => p.Tags)
                .ThenInclude(g => g.Tag)
                .Include(p => p.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefault(p => p.PostId == id);
            //var post = _postRepository.GetPostById(id);
           
            if (post == null)
                return NotFound();

            return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> AddPost()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.Blogs = new SelectList(_blogRepository.GetAllBlogs(user), "Id", "Title");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(string name, Post post)
        {
            var newFileName = string.Empty;

            if (HttpContext.Request.Form.Files != null)
            {
                var fileName = string.Empty;
                string PathDB = string.Empty;

                var files = HttpContext.Request.Form.Files;

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        //Getting FileName
                        fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                        //Getting file Extension
                        var FileExtension = Path.GetExtension(fileName);
                        // concating  FileName + FileExtension
                        newFileName = myUniqueFileName + FileExtension;
                        // Combines two strings into a path.
                        fileName = Path.Combine(_environment.WebRootPath, "demoImages") + $@"\{newFileName}";
                        // if you want to store path of folder in database
                        PathDB = "demoImages/" + newFileName;
                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User); 
                // post.Blog = _blogRepository.GetBlogByUser(user);
                post.PostUrl = newFileName;
                _postRepository.AddPost(post);
                return RedirectToAction("Index");
            }
            else
            {
                return View(post);
            }
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Post post = await _context.Posts.FirstOrDefaultAsync(p => p.PostId == id);
                if (post != null)
                    return View(post);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Post post = new Post { PostId = id.Value };
                _context.Entry(post).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Post");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult AddTagToPost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTagToPost(Tag tag, int id)
        {
            if (ModelState.IsValid)
            {
                var post = _postRepository.GetPostById(id);
                if (post == null)
                    return NotFound();
                //if (!post.Tags.Contains(tag))
                _tagRepository.AddTagToPost(tag, id);
                return RedirectToAction("Index");
            }
            else
            {
                return View(tag);
            }
        }

        [HttpGet]
        public IActionResult AddPostToTag()
        {
            ViewBag.Tags = new SelectList(_tagRepository.GetAllTags(), "TagId", "Title");
            return View();
        }

        [HttpPost]
        public IActionResult AddPostToTag(Tag tag, int id)
        {
            if (ModelState.IsValid)
            {
                var post = _context.Posts.FirstOrDefault(p => p.PostId == id);
                _context.AddRange(new PostTag { Post = post, Tag = tag });
                _context.SaveChanges();
                //var post = _postRepository.GetPostById(id);
                //if (post == null)
                //    return NotFound();
                
                return RedirectToAction("Index");
            }
            else
            {
                return View(tag);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int postId, string content)
        {
            if (content == null)
            {
                return RedirectToAction("Details", new { id = postId });
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var post = _postRepository.GetPostById(postId);

            Comment comment = new Comment()
            {
                User = user,
                Content = content
            };
            post.Comments.Add(comment);
            _context.SaveChanges();
            await _hubContext.Clients.All.SendAsync("ReceiveComment", user.UserName, content, postId);
            return RedirectToAction("Details", new { id = postId });
           
        }
    }
}
