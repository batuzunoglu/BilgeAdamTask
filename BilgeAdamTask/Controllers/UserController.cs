using BilgeAdamTask.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BilgeAdamTask.Controllers
{
    public class UserController : Controller
    {
        private readonly BlogContext _context;
        public UserController(
                BlogContext context
            )
        {
            _context = context;
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(User input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            var user = _context.Users.FirstOrDefault(x => x.Email == input.Email && x.Password == input.Password);
            if (user != null)
            {
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, (user.Name+" "+user.SurName)),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                };

                var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");

                var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
                HttpContext.SignInAsync(userPrincipal);

                return RedirectToAction("Index", "Home", new { area = "AdminPanel" });
            }
            ViewData["Errors"] = "Wrong Email or Password";
            return View();

        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
