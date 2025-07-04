﻿using Final_MozArt.Data;
using Final_MozArt.Models;
using Final_MozArt.Services.Interfaces;
using Final_MozArt.ViewModels.Product;
using Final_MozArt.ViewModels.Shop;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Final_MozArt.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductService _productService;
        private readonly AppDbContext _context;
        public WishlistService(IHttpContextAccessor httpContextAccessor, IProductService productService,
                                                                          AppDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
            _context = context;
        }

        //public int AddWishlist(int id, ProductVM product)
        //{
        //    List<WishlistVM> wishlist;

        //    if (_httpContextAccessor.HttpContext.Request.Cookies["wishlist"] != null)
        //    {
        //        wishlist = JsonConvert.DeserializeObject<List<WishlistVM>>(_httpContextAccessor.HttpContext.Request.Cookies["wishlist"]);
        //    }
        //    else
        //    {
        //        wishlist = new List<WishlistVM>();
        //    }



        //    WishlistVM existProducts = wishlist.FirstOrDefault(m => m.Id == product.Id);

        //    if (existProducts is null)
        //    {
        //        wishlist.Add(new WishlistVM { Id = product.Id });
        //    }


        //    _httpContextAccessor.HttpContext.Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlist));

        //    return wishlist.Count();
        //}
        public int AddWishlist(int id, ProductVM product)
        {
            List<WishlistVM> wishlist;

            var cookie = _httpContextAccessor.HttpContext.Request.Cookies["wishlist"];
            if (cookie != null)
            {
                wishlist = JsonConvert.DeserializeObject<List<WishlistVM>>(cookie);
            }
            else
            {
                wishlist = new List<WishlistVM>();
            }

            var existProduct = wishlist.FirstOrDefault(m => m.Id == id);
            if (existProduct == null)
            {
                wishlist.Add(new WishlistVM { Id = id });
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlist));

            return wishlist.Count;
        }


        public void DeleteItem(int id)
        {

            List<WishlistVM> wishlist = JsonConvert.DeserializeObject<List<WishlistVM>>(_httpContextAccessor.HttpContext.Request.Cookies["wishlist"]);

            WishlistVM wishlistItem = wishlist.FirstOrDefault(m => m.Id == id);

            wishlist.Remove(wishlistItem);

            _httpContextAccessor.HttpContext.Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlist));

        }

        public int GetCount()
        {
            List<WishlistVM> wishlist;

            if (_httpContextAccessor.HttpContext.Request.Cookies["wishlist"] != null)
            {
                wishlist = JsonConvert.DeserializeObject<List<WishlistVM>>(_httpContextAccessor.HttpContext.Request.Cookies["wishlist"]);
            }
            else
            {
                wishlist = new List<WishlistVM>();

            }

            return wishlist.Count();
        }

        public async Task<List<WishlistDetailVM>> GetWishlistDatasAsync()
        {
            List<WishlistVM> wishlist;

            if (_httpContextAccessor.HttpContext.Request.Cookies["wishlist"] != null)
            {
                wishlist = JsonConvert.DeserializeObject<List<WishlistVM>>(_httpContextAccessor.HttpContext.Request.Cookies["wishlist"]);
            }
            else
            {
                wishlist = new List<WishlistVM>();

            }

            List<WishlistDetailVM> wishlistDetails = new();
            foreach (var item in wishlist)
            {
                ProductVM existProduct = await _productService.GetByIdWithIncludesAsync(item.Id);

                wishlistDetails.Add(new WishlistDetailVM
                {
                    Id = existProduct.Id,
                    Name = existProduct.Name,
                    Price = existProduct.Price,
                    Image = existProduct.Image
                });
            }
            return wishlistDetails;
        }



        public List<WishlistVM> GetDatasFromCookies()
        {
            var data = _httpContextAccessor.HttpContext.Request.Cookies["wishlist"];

            if (data is not null)
            {
                var wishlist = JsonConvert.DeserializeObject<List<WishlistVM>>(data);
                return wishlist;
            }
            else
            {
                return new List<WishlistVM>();
            }

        }

        public void SetDatasToCookies(List<WishlistVM> wishlist, Product dbProduct, WishlistVM existProduct)
        {
            if (existProduct == null)
            {
                wishlist.Add(new WishlistVM
                {
                    Id = dbProduct.Id
                });
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlist));
        }

        public async Task<Wishlist> GetByUserIdAsync(string userId)
        {
            return await _context.Wishlists.Include(m => m.WishlistProducts).FirstOrDefaultAsync(m => m.AppUserId == userId);
        }

        public async Task<List<WishlistProduct>> GetAllByWishlistIdAsync(int? wishlistId)
        {
            return await _context.WishlistProducts.Where(m => m.WishlistId == wishlistId).ToListAsync();
        }

    }
}