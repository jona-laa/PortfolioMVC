using System.ComponentModel.DataAnnotations;

namespace PortfolioMVC.Models
{
    public class About
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Field is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Content { get; set; }
        
        [Required(ErrorMessage = "Choose an image")]
        public string Avatar { get; set; }

        public bool IsPublished { get; set; }
    }
}