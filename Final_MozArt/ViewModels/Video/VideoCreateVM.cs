using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.Video
{
    public class VideoCreateVM
    {
        [Required]
        public string VideoURL { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
