using System.Linq;
using PortfolioMVC.Data;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioMVC.ViewComponents
{
    public class Work : ViewComponent
    {
        private readonly ApplicationDbContext db;

        public Work(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IViewComponentResult Invoke()
        {
            var work = db.Work.ToArray();
            return View(work);
        }
    }
}