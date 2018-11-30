using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lolo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lolo.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        AppDbContext _context;
        public UserController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;

        }

        [HttpGet]
        public async Task<IEnumerable<IdentityUser>> Get()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            return _context.Users.Where(u => u != currentUser && u.Email != "admin@gmail.com").ToList();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            IdentityUser user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        // POST api/users
        [HttpPost]
        public IActionResult Post([FromBody]IdentityUser user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }

        // PUT api/users/
        [HttpPut]
        public IActionResult Put([FromBody]IdentityUser user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!_context.Users.Any(x => x.Id == user.Id))
            {
                return NotFound();
            }

            _context.Update(user);
            _context.SaveChanges();
            return Ok(user);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            IdentityUser user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok(user);
        }
    }
}
