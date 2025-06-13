using Final_MozArt.Services.Interfaces;
using Final_MozArt.ViewModels.Blog;
using Final_MozArt.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Final_MozArt.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            var totalCount = await _blogService.GetTotalBlogCountAsync();
            var blogs = await _blogService.GetBlogsAsync(skip: 0, take: 3);

            var model = new BlogUIVM
            {
                Blogs = blogs,
                TotalCount = totalCount
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LoadMore(int skip = 0, int take = 3)
        {
            try
            {
                var blogs = await _blogService.GetBlogsAsync(skip, take);

                var formattedBlogs = blogs.Select(b => new
                {
                    id = b.Id,
                    title = b.Title,
                    description = b.Description,
                    image = b.Image,
                    createDate = b.CreateDate,
                    blogCategoryName = b.BlogCategoryName 
                }).ToList();

                return Json(new { success = true, blogs = formattedBlogs });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LoadMore Error: {ex.Message}");
                return Json(new { success = false, message = ex.Message, blogs = new List<object>() });
            }
        }



        public async Task<IActionResult> Detail(int id)
        {
            var blogs = await _blogService.GetAllAsync();
            var blog = await _blogService.GetByIdAsync(id);
            BlogDetailVM model = new BlogDetailVM()
            {
              Blogs=blogs,
              Blog=blog
               
            };
            return View(model);
        }
    }
}