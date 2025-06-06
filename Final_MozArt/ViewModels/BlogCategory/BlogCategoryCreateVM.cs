using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.BlogCategory
{
    public class BlogCategoryCreateVM
    {
        [Required]
        public string Name { get; set; }

        
    }
}
