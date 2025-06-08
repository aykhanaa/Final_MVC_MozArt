using Final_MozArt.ViewModels.Product;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProductService
{
    // Oxuma əməliyyatları
    Task<List<ProductVM>> GetAllAsync();
    Task<ProductVM> GetByIdWithIncludesAsync(int id);
    Task<ProductDetailVM> GetByIdWithIncludesWithoutTrackingAsync(int id);
    Task<ProductVM> GetByNameWithoutTrackingAsync(string name);

    // Səhifələnmiş oxuma
    Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take);
    Task<List<ProductVM>> GetPaginatedDatasByCategory(int categoryId, int page, int take);

    // Axtarış və sıralama
    Task<List<ProductVM>> SearchAsync(string searchText, int page, int take);
    Task<List<ProductVM>> OrderByNameAsc(int page, int take);
    Task<List<ProductVM>> OrderByNameDesc(int page, int take);
    Task<List<ProductVM>> OrderByPriceAsc(int page, int take);
    Task<List<ProductVM>> OrderByPriceDesc(int page, int take);

    // Filtrləmə
    Task<List<ProductVM>> FilterAsync(int minPrice, int maxPrice);
    Task<int> FilterCountAsync(int minPrice, int maxPrice);

    // Say sayğacları
    Task<int> GetCountAsync();
    Task<int> GetCountByCategoryAsync(int categoryId);
    Task<int> GetCountBySearch(string searchText);

    // CRUD əməliyyatları
    Task CreateAsync(ProductCreateVM productCreateVM);
    Task EditAsync(ProductEditVM productEditVM);
    Task DeleteAsync(int id);

    // Şəkillərlə bağlı əməliyyatlar
    Task DeleteProductImageAsync(int imageId);
    Task SetMainImageAsync(SetIsMainVM data);
}
