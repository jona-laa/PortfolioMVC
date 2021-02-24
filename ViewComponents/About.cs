using System.Linq;
using PortfolioMVC.Data;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioMVC.ViewComponents
{
    public class About : ViewComponent
    {
        private readonly ApplicationDbContext db;

        public About(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IViewComponentResult Invoke()
        {
            var about = db.About
                .Where(p => p.IsPublished)
                .ToArray();
            
            return View(about);
        }
    }
}