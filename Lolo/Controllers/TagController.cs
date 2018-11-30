using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lolo.Models;
using Lolo.Models.Interfaces;
using Lolo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lolo.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly AppDbContext _context;


        public TagController(ITagRepository tagRepository, AppDbContext context)
        {
            _tagRepository = tagRepository;
            _context = context;
        }
        public IActionResult Index()
        {
            //var post = _context.Posts
            //   .Include(p => p.Tags)
            //   .ThenInclude(g => g.Tag)
            //   .FirstOrDefault(p => p.PostId == id);
            var tags = _context.PostTags
                .Include(p => p.Tag);
            var tagViewModel = new TagViewModel
            {
                Tags = tags.ToList()
            };
            // var tags = _tagRepository.GetAllTags();
            return View(tagViewModel);
        }
    }
}
