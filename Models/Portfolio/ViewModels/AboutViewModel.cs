using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http; 

namespace Models.Portfolio.ViewModels
{
    public class AboutViewModel
    {   
        [Required(ErrorMessage = "Field is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Field is required"), DataType(DataType.MultilineText)]
        public string Content { get; set; }
        
        [Required(ErrorMessage = "Choose an image")]
        public IFormFile Avatar { get; set; }
    }
}