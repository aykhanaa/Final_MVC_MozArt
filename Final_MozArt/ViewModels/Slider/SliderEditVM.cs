using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.Slider
{
    public class SliderEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public IFormFile? Photo { get; set; } 

        public string Image { get; set; } 
    }
}
