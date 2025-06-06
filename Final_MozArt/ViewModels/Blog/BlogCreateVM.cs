
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.Blog
{
    public class BlogCreateVM
    {
        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select a category")]
        public int BlogCategoryId { get; set; }

        [Required(ErrorMessage = "Please upload an image")]
        public IFormFile Photo { get; set; }
    }
}
