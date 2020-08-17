using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BilgeAdamTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeAdamTask.Areas.AdminPanel.Controllers
{
    [Authorize]
    [Area("AdminPanel")]
    public class HomeController : Controller
    {
        private readonly BlogContext _context;
        public HomeController(BlogContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["PostCount"] = _context.Posts.Count();
            ViewData["CategoryCount"] = _context.Categories.Count();
            return View();
        }
    }
}
