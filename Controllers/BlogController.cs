using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioMVC.Data;
using PortfolioMVC.Models;

namespace PortfolioMVC.Controllers
{
    [Route("Blog")]
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;


        public BlogController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Blog
        [HttpGet, Route("")]
        public IActionResult Index(int page = 0)
        {
            int pageSize = 2;
            double totalPosts = _context.Posts.Count();
            var totalPages = totalPosts / pageSize;
            double previousPage = page - 1;
            double nextPage = page + 1;

            ViewBag.PreviousPage = previousPage;
            ViewBag.HasPreviousPage = previousPage >= 0;
            ViewBag.NextPage = nextPage;
            ViewBag.HasNextPage = nextPage < totalPages;

            var posts =
                _context.Posts
                    .OrderByDescending(x => x.Posted)
                    .Skip(pageSize * page)
                    .Take(pageSize)
                    .ToArray();

            if(Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView(posts);
            
            return View(posts);
        }

        // GET: Blog
        [Authorize, Route("Admin")]
        public IActionResult Admin(int page = 0)
        {
            var pageSize = 2;
            double totalPosts = _context.Posts.Count();
            var totalPages = totalPosts / pageSize;
            var previousPage = page - 1;
            var nextPage = page + 1;

            ViewBag.PreviousPage = previousPage;
            ViewBag.HasPreviousPage = previousPage >= 0;
            ViewBag.NextPage = nextPage;
            ViewBag.HasNextPage = nextPage < totalPages;

            var posts =
                _context.Posts
                    .OrderByDescending(x => x.Posted)
                    .Skip(pageSize * page)
                    .Take(pageSize)
                    .ToArray();

            if(Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView(posts);
            
            return View(posts);
        }

        // GET: Single Blog Post
        [Route("{key}")]
        public IActionResult Post(string key) => View(_context.Posts.FirstOrDefault(p => p.Key == key));

        // GET: Blog/Create
        [Authorize, Route("Create")]
        public IActionResult Create() => View();

        // POST: Blog/Create
        [HttpPost, Route("Create")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Save image to wwwroot/images/uploads
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(blogPost.ImageFile.FileName);
                    string extension = Path.GetExtension(blogPost.ImageFile.FileName);
                    blogPost.ImageName = fileName = fileName + DateTime.Now.ToString("yymmddss") + extension;
                    var path = Path.Combine($"{wwwRootPath}/images/uploads/blog/", fileName);
                    
                    using(var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await blogPost.ImageFile.CopyToAsync(fileStream);
                    }
                }
                catch
                {
                    ViewBag.ImageError = "Please upload image too, would ya? Mkay.";
                    return View();
                }

                blogPost.Author = User.Identity.Name;
                blogPost.Posted = DateTime.Now;

                _context.Add(blogPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Admin));
            }
            return View(blogPost);
        }

        // GET: Blog/Edit/5
        [Authorize, Route("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.Posts.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            return View(blogPost);
        }

        // POST: Blog/Edit/5
        [HttpPost, Route("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Key,Title,ImageName,ImageDescription,ImageAltText,Author,Body,Posted")] BlogPost blogPost)
        {
            if (id != blogPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // If New Image Uploaded
                        // Get imagePath of old image with FileName
                        
                        // Delete old image file

                        // Create new fileName with ImageFile name
                        // set blogPost.ImageName with created name
                        // Save image to images/uploads

                    blogPost.Author = User.Identity.Name;
                    _context.Update(blogPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostExists(blogPost.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Admin));
            }
            return View(blogPost);
        }

        // GET: Blog/Delete/5
        [Authorize, Route("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete"), Route("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogPost = await _context.Posts.FindAsync(id);

            // Delete image from wwwroot/images/uploads
            try
            {
                var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images/uploads/blog", blogPost.ImageName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            catch
            {
                Console.Write("Something went wrong");
            }

            _context.Posts.Remove(blogPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Admin));
        }

        private bool BlogPostExists(int id) => _context.Posts.Any(e => e.Id == id);
    }
}
