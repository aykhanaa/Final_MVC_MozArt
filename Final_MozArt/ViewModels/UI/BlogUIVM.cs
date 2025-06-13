using Final_MozArt.ViewModels.Blog;
using Final_MozArt.ViewModels.Slider;

namespace Final_MozArt.ViewModels.UI
{
    public class BlogUIVM
    {
        public IEnumerable<BlogVM>  Blogs { get; set; }
        public int TotalCount { get; set; }
    }
}
