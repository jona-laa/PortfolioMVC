using System.ComponentModel.DataAnnotations;

namespace PortfolioMVC.Models
{
    public class Skill
    {
        public int Id { get; set; }
        
        [Display(Name = "Skill"), Required(ErrorMessage = "Field is required")]
        public string SkillName { get; set; }

        [RegularExpression(@"^(fas)\s(fa-)[a-z0-9\-]+|^(fab\sfa-)[a-z0-9\-]+"), Required(ErrorMessage =" Field is required")]
        public string Icon { get; set; }
    }
}