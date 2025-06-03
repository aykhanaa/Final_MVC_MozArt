using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.Brand
{
    public class BrandCreateVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
