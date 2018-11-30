using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lolo.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lolo.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly ITagRepository _tagRepository;

        public HomeController(IStringLocalizer<HomeController> localizer,
            UserManager<IdentityUser> userManager,
            IStringLocalizer<SharedResource> sharedLocalizer,
            ITagRepository tagRepository)
        {
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;
            _userManager = userManager;
            _tagRepository = tagRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users()
        {
            //var users = _userManager.Users;
            //var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            //users = users.Where(u => u != currentUser && u.Email != "admin@gmail.com");

            return View();
        }
        
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }
        
    }
}
