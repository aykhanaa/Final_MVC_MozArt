using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.Category
{
    public class CategoryCreateVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
