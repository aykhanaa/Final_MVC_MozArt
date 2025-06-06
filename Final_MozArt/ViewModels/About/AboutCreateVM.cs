using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.About
{
    public class AboutCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Subtitle { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
