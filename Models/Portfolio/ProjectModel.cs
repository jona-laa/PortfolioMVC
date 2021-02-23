using System.ComponentModel.DataAnnotations;

namespace PortfolioMVC.Models
{
    public class Project
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Field is required")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Choose an image")]
        public string Image { get; set; }
    }
}