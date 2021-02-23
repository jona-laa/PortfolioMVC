using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http; 

namespace Models.Portfolio.ViewModels
{
    public class ProjectViewModel
    {
        
        [Required(ErrorMessage = "Field is required")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Choose an image")]
        public IFormFile Image { get; set; }
    }
}