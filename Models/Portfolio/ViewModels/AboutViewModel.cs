using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http; 

namespace PortfolioMVC.Models
{
    public class AboutViewModel
    {   
        [Required(ErrorMessage = "Field is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Field is required"), DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Required(ErrorMessage = "Field is required"), Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; }

        public bool IsPublished { get; set; }
    }
}