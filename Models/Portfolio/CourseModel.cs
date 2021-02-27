using System.ComponentModel.DataAnnotations;
using System;

namespace PortfolioMVC.Models
{
    public class Course
    {
         public int Id { get; set; }
        
        [Required(ErrorMessage = "Field is required"), Display(Name = "Course Name")]
        public string CourseName { get; set; }
        
        [Required(ErrorMessage = "Field is required")]
        public string Institution { get; set; }
        
        [Required(ErrorMessage = "Field is required"), Display(Name = "Start Date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}"), DataType(DataType.Date)]
        public DateTime DateStart { get; set; }
        
        [Required(ErrorMessage = "Field is required"), Display(Name = "End Date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}"), DataType(DataType.Date)]
        public DateTime DateEnd { get; set; }
        
        [Required(ErrorMessage = "Field is required"), DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}