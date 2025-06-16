using AutoMapper;
using Final_MozArt.Data;
using Final_MozArt.Models;
using Final_MozArt.Services.Interfaces;
using Final_MozArt.ViewModels.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Final_MozArt.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context; // Burada dəyişdirdik
        private readonly IMapper _mapper;
        private readonly string _imageFolderPath = "wwwroot/assets/img/product-details";

        public ProductService(AppDbContext context, IMapper mapper) // Konstruktor parametri də dəyişdi
        {
            _context = context;
            _mapper = mapper;

            if (!Directory.Exists(_imageFolderPath))
                Directory.CreateDirectory(_imageFolderPath);
        }

        public async Task<ICollection<ProductVM>> GetAllAsync()
        {
            var products = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Images)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<ICollection<ProductVM>>(products);
        }

        public async Task<ProductVM> GetByIdWithIncludesAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Images)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return null;

            return _mapper.Map<ProductVM>(product);
        }

        public async Task<ProductDetailVM> GetByIdWithIncludesWithoutTrackingAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.ProductColors).ThenInclude(pc => pc.Color)
                .Include(p => p.ProductTags).ThenInclude(pt => pt.Tag)
                .Include(p => p.Images)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return null;

            var detailVM = new ProductDetailVM
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CreateDate = product.CreateDate,

                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name,

                BrandId = product.BrandId,
                BrandName = product.Brand?.Name,

                ColorIds = product.ProductColors.Select(pc => pc.ColorId).ToList(),
                ColorNames = product.ProductColors.Select(pc => pc.Color.Name).ToList(),

                TagIds = product.ProductTags.Select(pt => pt.TagId).ToList(),
                TagNames = product.ProductTags.Select(pt => pt.Tag.Name).ToList(),

                Images = product.Images?.ToList()
            };

            return detailVM;
        }


        public async Task<ICollection<ProductVM>> GetPaginatedDatasByCategory(int categoryId, int page, int take)
        {
            var products = await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Images)
                .AsNoTracking()
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();

            return _mapper.Map<ICollection<ProductVM>>(products);
        }

        public async Task<ICollection<ProductVM>> GetPaginatedDatasAsync(int page, int take)
        {
            var products = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Images)
                .AsNoTracking()
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();

            return _mapper.Map<ICollection<ProductVM>>(products);
        }

        public async Task<ProductVM> GetByNameWithoutTrackingAsync(string name)
        {
            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Images)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());

            return product == null ? null : _mapper.Map<ProductVM>(product);
        }

        public async Task<int> GetCountAsync()
            => await _context.Products.CountAsync();

        public async Task<int> GetCountByCategoryAsync(int categoryId)
            => await _context.Products.Where(p => p.CategoryId == categoryId).CountAsync();

        public async Task<int> GetCountBySearch(string searchText)
            => await _context.Products
                .Where(p => p.Name.Contains(searchText) || p.Description.Contains(searchText))
                .CountAsync();

        public async Task<ICollection<ProductVM>> SearchAsync(string searchText, int page, int take)
        {
            var products = await _context.Products
                .Where(p => p.Name.Contains(searchText) || p.Description.Contains(searchText))
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Images)
                .AsNoTracking()
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();

            return _mapper.Map<ICollection<ProductVM>>(products);
        }

        public async Task<ICollection<ProductVM>> OrderByNameAsc(int page, int take)
        {
            var products = await _context.Products
                .OrderBy(p => p.Name)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Images)
                .AsNoTracking()
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();

            return _mapper.Map<ICollection<ProductVM>>(products);
        }

        public async Task<ICollection<ProductVM>> OrderByNameDesc(int page, int take)
        {
            var products = await _context.Products
                .OrderByDescending(p => p.Name)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Images)
                .AsNoTracking()
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();

            return _mapper.Map<ICollection<ProductVM>>(products);
        }

        public async Task<ICollection<ProductVM>> OrderByPriceAsc(int page, int take)
        {
            var products = await _context.Products
                .OrderBy(p => p.Price)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Images)
                .AsNoTracking()
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();

            return _mapper.Map<ICollection<ProductVM>>(products);
        }

        public async Task<ICollection<ProductVM>> OrderByPriceDesc(int page, int take)
        {
            var products = await _context.Products
                .OrderByDescending(p => p.Price)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Images)
                .AsNoTracking()
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();

            return _mapper.Map<ICollection<ProductVM>>(products);
        }

        public async Task<ICollection<ProductVM>> FilterAsync(int minPrice, int maxPrice)
        {
            var products = await _context.Products
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.Images)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<ICollection<ProductVM>>(products);
        }

        public async Task<int> FilterCountAsync(int minPrice, int maxPrice)
            => await _context.Products
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .CountAsync();

        public async Task CreateAsync(ProductCreateVM vm)
        {
            var product = _mapper.Map<Product>(vm);

            product.Images = new List<ProductImage>();

            product.ProductColors = vm.ColorIds.Select(colorId => new ProductColor
            {
                ColorId = colorId,
                Product = product
            }).ToList();

            product.ProductTags = vm.TagIds.Select(tagId => new ProductTag
            {
                TagId = tagId,
                Product = product
            }).ToList();

            var photos = vm.Photos.ToList();
            for (int i = 0; i < photos.Count; i++)
            {
                var photo = photos[i];
                var fileName = await SaveFileAsync(photo);
                product.Images.Add(new ProductImage
                {
                    Image = fileName,
                    IsMain = i == 0,
                    IsHover = i == 1,
                    Product = product
                });
            }

           

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }




        public async Task EditAsync(ProductEditVM vm)
        {
            var product = await _context.Products
                .Include(p => p.ProductColors)
                .Include(p => p.ProductTags)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == vm.Id);

            if (product == null)
                throw new KeyNotFoundException("Product tapilmadi.");

            product.Name = vm.Name;
            product.Price = vm.Price;
            product.Description = vm.Description;
            product.BrandId = vm.BrandId;
            product.CategoryId = vm.CategoryId;

            _context.ProductColors.RemoveRange(product.ProductColors);
            product.ProductColors = vm.ColorIds.Select(colorId => new ProductColor
            {
                ColorId = colorId,
                ProductId = product.Id
            }).ToList();

            _context.ProductTags.RemoveRange(product.ProductTags);
            product.ProductTags = vm.TagIds.Select(tagId => new ProductTag
            {
                TagId = tagId,
                ProductId = product.Id
            }).ToList();

            if (vm.Photos != null && vm.Photos.Any())
            {
                product.Images.Clear();
                var photos = vm.Photos.ToList();
                for (int i = 0; i < photos.Count; i++)
                {
                    var photo = photos[i];
                    var fileName = await SaveFileAsync(photo);
                    product.Images.Add(new ProductImage
                    {
                        Image = fileName,
                        IsMain = i == 0,
                        IsHover = i == 1,
                        Product = product
                    });
                }
            }

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                throw new KeyNotFoundException("Product tapilmadi.");

            foreach (var image in product.Images)
            {
                DeleteFileIfExists(image.Image);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductImageAsync(int id)
        {
            var image = await _context.ProductImages.FindAsync(id);
            if (image == null)
                throw new KeyNotFoundException("Image tapilmadi.");

            DeleteFileIfExists(image.Image);

            _context.ProductImages.Remove(image);
            await _context.SaveChangesAsync();
        }


        public async Task SetMainImageAsync(SetIsMainVM data)
        {
            var product = await _context.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == data.ProductId);

            if (product == null)
                throw new KeyNotFoundException("Product tapilmadi.");

            var selectedImage = product.Images.FirstOrDefault(x => x.Id == data.ImageId);

            if (selectedImage == null)
                throw new KeyNotFoundException("Image tapilmadi.");

            if (selectedImage.IsHover)
            {
                // Hover şəkil main olaraq seçilə bilməz, sadəcə çıxış
                return;
            }

            foreach (var img in product.Images)
            {
                img.IsMain = img.Id == data.ImageId;
            }

            await _context.SaveChangesAsync();
        }


        private async Task<string> SaveFileAsync(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(_imageFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        private void DeleteFileIfExists(string fileName)
        {
            var filePath = Path.Combine(_imageFolderPath, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
        public async Task<List<Product>> SearchProductsAsync(string query)
        {
            IQueryable<Product> products = _context.Products.Include(p => p.Category);

            if (!string.IsNullOrEmpty(query))
            {
                query = query.ToLower();

                products = products.Where(p =>
                    p.Name.ToLower().Contains(query) ||
                    p.Category.Name.ToLower().Contains(query));
            }

            return await products.ToListAsync();
        }

    }
}
