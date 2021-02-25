using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PortfolioMVC.Data;
using PortfolioMVC.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace PortfolioMVC.Controllers
{
    public class AboutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AboutController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: About
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.About.ToListAsync());
        }

        // // GET: About/Details/5
        // [Authorize]
        // public async Task<IActionResult> Details(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var about = await _context.About
        //         .FirstOrDefaultAsync(m => m.Id == id);
        //     if (about == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(about);
        // }

        // GET: About/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: About/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,ImageFile,IsPublished")] About about)
        {
            if (ModelState.IsValid)
            {
                // Save image to wwwroot/images/uploads
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(about.ImageFile.FileName);
                string extension = Path.GetExtension(about.ImageFile.FileName);
                about.ImageName = fileName = fileName + DateTime.Now.ToString("yymmddss") + extension;
                var path = Path.Combine($"{wwwRootPath}/images/uploads/", fileName);
                
                using(var fileStream = new FileStream(path, FileMode.Create))
                {
                    await about.ImageFile.CopyToAsync(fileStream);
                }

                _context.Add(about);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(about);
        }

        // GET: About/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var about = await _context.About.FindAsync(id);
            if (about == null)
            {
                return NotFound();
            }
            return View(about);
        }

        // POST: About/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,ImageName,IsPublished")] About about)
        {
            if (id != about.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(about);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutExists(about.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(about);
        }

        // GET: About/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var about = await _context.About
                .FirstOrDefaultAsync(m => m.Id == id);
            if (about == null)
            {
                return NotFound();
            }

            return View(about);
        }

        // POST: About/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var about = await _context.About.FindAsync(id);

            // Delete image from wwwroot/images/uploads
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images/uploads", about.ImageName);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _context.About.Remove(about);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutExists(int id)
        {
            return _context.About.Any(e => e.Id == id);
        }
    }
}
