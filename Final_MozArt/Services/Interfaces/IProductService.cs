using Final_MozArt.Models;
using Final_MozArt.ViewModels.Product;



public interface IProductService
{
    Task<ICollection<ProductVM>> GetAllAsync();
    Task<ProductVM> GetByIdWithIncludesAsync(int id);
    Task<ProductDetailVM> GetByIdWithIncludesWithoutTrackingAsync(int id);
    Task<ICollection<ProductVM>> GetPaginatedDatasByCategory(int id, int page, int take);
    Task<ICollection<ProductVM>> GetPaginatedDatasAsync(int page, int take);
    Task<ProductVM> GetByNameWithoutTrackingAsync(string name);
    Task<int> GetCountAsync();
    Task<int> GetCountByCategoryAsync(int id);
    Task<int> GetCountBySearch(string searchText);
    Task<ICollection<ProductVM>> SearchAsync(string searchText, int page, int take);
    Task<ICollection<ProductVM>> OrderByNameAsc(int page, int take);
    Task<ICollection<ProductVM>> OrderByNameDesc(int page, int take);
    Task<ICollection<ProductVM>> OrderByPriceAsc(int page, int take);
    Task<ICollection<ProductVM>> OrderByPriceDesc(int page, int take);
    Task<ICollection<ProductVM>> FilterAsync(int value1, int value2);
    Task<int> FilterCountAsync(int value1, int value2);
    Task CreateAsync(ProductCreateVM product);
    Task EditAsync(ProductEditVM product);
    Task DeleteAsync(int id);
    Task DeleteProductImageAsync(int id);
    Task SetMainImageAsync(SetIsMainVM data);
    Task<List<Product>> SearchProductsAsync(string query);
}


