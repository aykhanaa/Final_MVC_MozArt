using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.Slider
{
    public class SliderCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
