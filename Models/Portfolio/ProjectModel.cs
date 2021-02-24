using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http; 

namespace PortfolioMVC.Models
{
    public class Project
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Field is required")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Field is required"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Image Name"), Column(TypeName = "nvarchar(100)")]
        public string ImageName { get; set; }

        [NotMapped, Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; }
    }
}