using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BilgeAdamTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BilgeAdamTask.Areas.AdminPanel.Controllers
{
    [Authorize]
    [Area("AdminPanel")]
    public class PostsController : Controller
    {
        private readonly BlogContext _context;
        private readonly IWebHostEnvironment _environment;      //bilgisayardaki rootpathleri ayarlar
        public PostsController(BlogContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: AdminPanel/Posts
        public IActionResult Index()
        {
            var blogContext = _context.Posts.Include(p => p.Category).ToList();
            return View(blogContext);
        }


        // GET: AdminPanel/Posts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }


        [HttpPost]
        public IActionResult Create(Post post, IFormFile file)
        {
            post.ImageUrl = SaveFile(file);
            post.UserId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            post.PublishDate = DateTime.Now;
            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", post.CategoryId);
                return View(post);
            }
            _context.Add(post);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: AdminPanel/Posts/Edit/5
        public IActionResult Edit(int id)
        {

            var post = _context.Posts.FirstOrDefault(x => x.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", post.CategoryId);
            return View(post);
        }

        // POST: AdminPanel/Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Edit(Post post, IFormFile file)
        {
            var newImage = SaveFile(file);
            if (newImage != null)
            {
                post.ImageUrl = newImage;
            }
            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", post.CategoryId);
                return View(post);
            }
            post.PublishDate = DateTime.Now;
            post.UserId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            _context.Update(post);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: AdminPanel/Posts/Delete/5
        public IActionResult Delete(int id)
        {
            var post = _context.Posts.FirstOrDefault(x=> x.Id == id);
            if (post==null)
            {
                return NotFound();
            }
            _context.Posts.Remove(post);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        private string SaveFile(IFormFile file)
        {
            if (file != null)
            {
                // burada dilerseniz dosya tipine gore filtreleme yaparak sadece istediginiz dosya formatindaki dosyalari yukleyebilirsiniz
                if (file.ContentType == "image/jpeg" || file.ContentType == "image/jpg" || file.ContentType == "image/png" || file.ContentType == "image/gif")
                {
                    //dosya bilgilerini alır.
                    var fi = new FileInfo(file.FileName);
                    
                    //dosya ismi alir.
                    var fileName = Path.GetFileName(file.FileName);
                    
                    //dosya ismine uniq isim verir.
                    fileName = Guid.NewGuid().ToString() + fi.Extension;

                    //resmin yazılacağı pathi belirler .
                    var path = Path.Combine(_environment.WebRootPath, "Uploads", fileName);

                    //resim belirlenen pathe yazılır.
                    file.CopyTo(new FileStream(path, FileMode.Create));
                    return "/Uploads/" + fileName;
                }
            }
            return null;
        }
    }

}
