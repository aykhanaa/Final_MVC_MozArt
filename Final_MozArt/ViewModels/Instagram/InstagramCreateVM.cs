using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.Instagram
{
    public class InstagramCreateVM
    {
        [Required]
        public IFormFile Photo { get; set; }
    }
}
