using Final_MozArt.ViewModels.Category;
using Final_MozArt.ViewModels.Slider;

namespace Final_MozArt.ViewModels.UI
{
    public class HomeVM
    {
        public IEnumerable<SliderVM> Sliders { get; set; }
        public IEnumerable<CategoryVM> Categories { get; set; }

    }
}
