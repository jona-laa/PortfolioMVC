using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortfolioMVC.Data;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioMVC.ViewComponents
{
    public class Skills : ViewComponent
    {
        private readonly ApplicationDbContext db;

        public Skills(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IViewComponentResult Invoke()
        {
            var skills = db.Skills.ToArray();
            return View(skills);
        }
    }
}