using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioMVC.Data;
using PortfolioMVC.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace PortfolioMVC.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _hostEnvironment;

        public ProjectController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Project
        [Authorize]
        public async Task<IActionResult> Index() => View(await _context.Projects.ToListAsync());

        // GET: Project/Create
        [Authorize]
        public IActionResult Create() => View();

        // POST: Project/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Url,Title,Description,ImageFile,ImageDescription")] Project project)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Save image to wwwroot/images/uploads
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(project.ImageFile.FileName);
                    string extension = Path.GetExtension(project.ImageFile.FileName);
                    project.ImageName = fileName = fileName + DateTime.Now.ToString("yymmddss") + extension;
                    var path = Path.Combine($"{wwwRootPath}/images/uploads/portfolio/", fileName);
                    
                    using(var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await project.ImageFile.CopyToAsync(fileStream);
                    }   
                }
                catch
                {
                    ViewBag.ImageError = "Please upload image too, would ya? Mkay.";
                    return View();
                }
                
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Project/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Project/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Url,Title,Description,ImageName,ImageDescription")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            return View(project);
        }

        // GET: Project/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            // Delete image from wwwroot/images/uploads
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images/uploads/portfolio", project.ImageName);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id) => _context.Projects.Any(e => e.Id == id);
    }
}
