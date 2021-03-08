using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http; 

namespace PortfolioMVC.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        public string _key;

        public string Key
        {
            get
            {
                if (_key == null)
                {
                    _key = Regex.Replace(Title.ToLower(), "[^a-z0-9]", "-");
                }
                return _key;
            }
            set { _key = value; }
        }

        [Required, DataType(DataType.Text)]
        public string Title { get; set; }

        [Display(Name = "Image Name"), Column(TypeName = "nvarchar(100)")]
        public string ImageName { get; set; }

        [Display(Name = "Upload Image"), NotMapped]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Image Description"), Required]
        public string ImageDescription { get; set; }

        [Display(Name = "Image Alt-text"), Required]
        public string ImageAltText { get; set; }

        public string Author { get; set; }

        [Required, Display(Name = "Content"), DataType(DataType.MultilineText)]
        public string Body { get; set; }
        
        public DateTime Posted { get; set; }
    }
}