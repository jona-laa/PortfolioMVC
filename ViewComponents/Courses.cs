using System.Linq;
using PortfolioMVC.Data;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioMVC.ViewComponents
{
    public class Courses : ViewComponent
    {
        private readonly ApplicationDbContext db;

        public Courses(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IViewComponentResult Invoke()
        {
            var courses = db.Courses
                .OrderByDescending(x => x.DateStart)
                .ToArray();
            
            return View(courses);
        }
    }
}