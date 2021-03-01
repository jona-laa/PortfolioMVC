using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http; 

namespace PortfolioMVC.Models
{
    public class About
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Field is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Field is required"), DataType(DataType.MultilineText)]
        public string Content { get; set; }
        
        [Display(Name = "Image Name"), Column(TypeName = "nvarchar(100)")]
        public string ImageName { get; set; }

        [NotMapped, Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Image Description"), Required]
        public string ImageDescription { get; set; }

        public bool IsPublished { get; set; }
    }
} 