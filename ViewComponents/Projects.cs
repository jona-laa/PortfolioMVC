using System.Linq;
using PortfolioMVC.Data;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioMVC.ViewComponents
{
    public class Projects : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public Projects(ApplicationDbContext db)
        {
            this._db = db;
        }

        public IViewComponentResult Invoke()
        {
            var projects = _db.Projects.ToArray();
            return View(projects);
        }
    }
}