using Final_MozArt.Models;

namespace Final_MozArt.ViewModels.Product
{
    public class ProductDetailVM
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public string CategoryName { get; set; }
        public int CategoryId { get; set; }

        public string BrandName { get; set; }
        public int BrandId { get; set; }

        public List<string> TagNames { get; set; }
        public List<string> ColorNames { get; set; }

        public List<ProductImageVM> Images { get; set; }

        public DateTime CreateDate { get; set; }
    }

}
