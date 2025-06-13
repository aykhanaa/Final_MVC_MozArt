using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.Product
{
    public class ProductCreateVM
    {
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

        [Required]
        public ICollection<int> ColorIds { get; set; }

        [Required]
        public ICollection<int> TagIds { get; set; }

        [Required]
        public ICollection<IFormFile> Photos { get; set; }
    }
}
