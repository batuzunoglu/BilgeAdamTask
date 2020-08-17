using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BilgeAdamTask.Models;
using Microsoft.EntityFrameworkCore;

namespace BilgeAdamTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogContext _context;

        public HomeController(BlogContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var result = _context.Posts.Include(x => x.Category).ToList();
            ViewData["CategoryList"] = _context.Categories.Take(20).ToList();

            return View(result);
        }

        public IActionResult Detail(int Id)
        {
            var post = _context.Posts.Include(x => x.Category).Include(x => x.User).FirstOrDefault(x => x.Id == Id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }
          
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
