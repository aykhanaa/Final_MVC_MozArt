using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.About
{
    public class AboutEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Subtitle { get; set; }

        [Required]
        public string Description { get; set; }

        public IFormFile? Photo { get; set; }

        public string Image { get; set; }
    }
}
