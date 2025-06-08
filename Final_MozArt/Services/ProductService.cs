using AutoMapper;
using Final_MozArt.Data;
using Final_MozArt.Models;
using Final_MozArt.ViewModels.Product;
using Microsoft.EntityFrameworkCore;


public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProductService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private async Task<string> UploadImageAsync(IFormFile photo)
    {
        if (photo == null || photo.Length == 0) return null;

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await photo.CopyToAsync(fileStream);
        }

        return "/images/products/" + uniqueFileName;
    }

    public async Task<List<ProductVM>> GetAllAsync()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.Images)
            .ToListAsync();

        return _mapper.Map<List<ProductVM>>(products);
    }

    public async Task<ProductVM> GetByIdWithIncludesAsync(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == id);

        return _mapper.Map<ProductVM>(product);
    }

    public async Task<ProductDetailVM> GetByIdWithIncludesWithoutTrackingAsync(int id)
    {
        var product = await _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.Images)
            .Include(p => p.ProductTags).ThenInclude(pt => pt.Tag)
            .Include(p => p.ProductColors).ThenInclude(pc => pc.Color)
            .FirstOrDefaultAsync(p => p.Id == id);

        return _mapper.Map<ProductDetailVM>(product);
    }

    public async Task<ProductVM> GetByNameWithoutTrackingAsync(string name)
    {
        var product = await _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Name == name);

        return _mapper.Map<ProductVM>(product);
    }

    public async Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take)
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Images)
            .Skip((page - 1) * take)
            .Take(take)
            .ToListAsync();

        return _mapper.Map<List<ProductVM>>(products);
    }

    public async Task<List<ProductVM>> GetPaginatedDatasByCategory(int categoryId, int page, int take)
    {
        var products = await _context.Products
            .Where(p => p.CategoryId == categoryId)
            .Include(p => p.Category)
            .Include(p => p.Images)
            .Skip((page - 1) * take)
            .Take(take)
            .ToListAsync();

        return _mapper.Map<List<ProductVM>>(products);
    }

    public async Task<List<ProductVM>> SearchAsync(string searchText, int page, int take)
    {
        var query = _context.Products
            .Where(p => p.Name.Contains(searchText))
            .Include(p => p.Images)
            .Include(p => p.Category);

        return _mapper.Map<List<ProductVM>>(await query.Skip((page - 1) * take).Take(take).ToListAsync());
    }

    public async Task<int> GetCountAsync() => await _context.Products.CountAsync();

    public async Task<int> GetCountByCategoryAsync(int categoryId)
        => await _context.Products.CountAsync(p => p.CategoryId == categoryId);

    public async Task<int> GetCountBySearch(string searchText)
        => await _context.Products.CountAsync(p => p.Name.Contains(searchText));

    public async Task<List<ProductVM>> OrderByNameAsc(int page, int take)
    {
        return _mapper.Map<List<ProductVM>>(await _context.Products
            .OrderBy(p => p.Name)
            .Include(p => p.Images)
            .Skip((page - 1) * take).Take(take).ToListAsync());
    }

    public async Task<List<ProductVM>> OrderByNameDesc(int page, int take)
    {
        return _mapper.Map<List<ProductVM>>(await _context.Products
            .OrderByDescending(p => p.Name)
            .Include(p => p.Images)
            .Skip((page - 1) * take).Take(take).ToListAsync());
    }

    public async Task<List<ProductVM>> OrderByPriceAsc(int page, int take)
    {
        return _mapper.Map<List<ProductVM>>(await _context.Products
            .OrderBy(p => p.Price)
            .Include(p => p.Images)
            .Skip((page - 1) * take).Take(take).ToListAsync());
    }

    public async Task<List<ProductVM>> OrderByPriceDesc(int page, int take)
    {
        return _mapper.Map<List<ProductVM>>(await _context.Products
            .OrderByDescending(p => p.Price)
            .Include(p => p.Images)
            .Skip((page - 1) * take).Take(take).ToListAsync());
    }

    public async Task<List<ProductVM>> FilterAsync(int minPrice, int maxPrice)
    {
        var products = await _context.Products
            .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
            .Include(p => p.Images)
            .ToListAsync();

        return _mapper.Map<List<ProductVM>>(products);
    }

    public async Task<int> FilterCountAsync(int minPrice, int maxPrice)
    {
        return await _context.Products.CountAsync(p => p.Price >= minPrice && p.Price <= maxPrice);
    }

    public async Task CreateAsync(ProductCreateVM productCreateVM)
    {
        var product = _mapper.Map<Product>(productCreateVM);
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        if (productCreateVM.SelectedColorIds != null)
        {
            foreach (var colorId in productCreateVM.SelectedColorIds)
            {
                product.ProductColors.Add(new ProductColor { ColorId = colorId, ProductId = product.Id });
            }
        }

        if (productCreateVM.SelectedTagIds != null)
        {
            foreach (var tagId in productCreateVM.SelectedTagIds)
            {
                product.ProductTags.Add(new ProductTag { TagId = tagId, ProductId = product.Id });
            }
        }

        if (productCreateVM.Photos != null && productCreateVM.Photos.Any())
        {
            bool isFirst = true;
            foreach (var photo in productCreateVM.Photos)
            {
                var imageUrl = await UploadImageAsync(photo);
                if (imageUrl != null)
                {
                    product.Images.Add(new ProductImage { Image = imageUrl, IsMain = isFirst });
                    isFirst = false;
                }
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task EditAsync(ProductEditVM productEditVM)
    {
        var product = await _context.Products
            .Include(p => p.Images)
            .Include(p => p.ProductColors)
            .Include(p => p.ProductTags)
            .FirstOrDefaultAsync(p => p.Id == productEditVM.Id);

        if (product == null) return;

        _mapper.Map(productEditVM, product);

        product.ProductColors.Clear();
        if (productEditVM.SelectedColorIds != null)
        {
            foreach (var colorId in productEditVM.SelectedColorIds)
            {
                product.ProductColors.Add(new ProductColor { ColorId = colorId, ProductId = product.Id });
            }
        }

        product.ProductTags.Clear();
        if (productEditVM.SelectedTagIds != null)
        {
            foreach (var tagId in productEditVM.SelectedTagIds)
            {
                product.ProductTags.Add(new ProductTag { TagId = tagId, ProductId = product.Id });
            }
        }

        if (productEditVM.NewPhotos != null && productEditVM.NewPhotos.Any())
        {
            foreach (var photo in productEditVM.NewPhotos)
            {
                var imageUrl = await UploadImageAsync(photo);
                if (imageUrl != null)
                {
                    product.Images.Add(new ProductImage { Image = imageUrl, IsMain = false });
                }
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return;

        foreach (var image in product.Images)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.Image.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            if (File.Exists(filePath)) File.Delete(filePath);
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductImageAsync(int imageId)
    {
        var image = await _context.ProductImages.FindAsync(imageId);
        if (image == null) return;

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.Image.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
        if (File.Exists(filePath)) File.Delete(filePath);

        _context.ProductImages.Remove(image);
        await _context.SaveChangesAsync();
    }

    public async Task SetMainImageAsync(SetIsMainVM data)
    {
        var product = await _context.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == data.ProductId);
        if (product == null) return;

        foreach (var img in product.Images)
        {
            img.IsMain = img.Id == data.ImageId;
        }

        await _context.SaveChangesAsync();
    }
}
