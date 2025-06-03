using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.Category
{
    public class CategoryEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IFormFile? Photo { get; set; }

        public string Image { get; set; }
    }
}
