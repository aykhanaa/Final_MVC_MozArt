using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.BlogCategory
{
    public class BlogCategoryEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
