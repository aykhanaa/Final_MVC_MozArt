using Final_MozArt.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.Product
{
    public class ProductEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Qiymət 0-dan böyük olmalıdır.")]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int BrandId { get; set; }

        public ICollection<int> ColorIds { get; set; }
        public ICollection<int> TagIds { get; set; }

        public ICollection<ProductImage> Images { get; set; }
        public ICollection<IFormFile> Photos { get; set; }
    }
}
