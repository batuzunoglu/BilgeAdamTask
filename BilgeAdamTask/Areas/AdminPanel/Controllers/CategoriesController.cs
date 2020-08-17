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
    public class CategoriesController : Controller
    {
        private readonly BlogContext _context;
        public CategoriesController(BlogContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _context.Categories.Add(category);
            //_context.Add<Category>(category);              ikisi de database'e ekler
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public IActionResult Delete(int Id)
        {
           var result = _context.Categories.FirstOrDefault(x => x.Id == Id);
            if(result == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(result);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _context.Categories.Update(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
