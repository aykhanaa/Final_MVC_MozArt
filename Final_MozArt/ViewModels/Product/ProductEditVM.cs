using Final_MozArt.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.Product
{
    public class ProductEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public SelectList Categories { get; set; }

        [Required]
        public int BrandId { get; set; }
        public SelectList Brands { get; set; }

        public List<int> SelectedColorIds { get; set; }
        public MultiSelectList Colors { get; set; }

        public List<int> SelectedTagIds { get; set; }
        public MultiSelectList Tags { get; set; }

        public List<ProductImageVM> ExistingImages { get; set; }
        public List<IFormFile> NewPhotos { get; set; }

        public bool IsPriceValid() => Price > 0;
    }

}
