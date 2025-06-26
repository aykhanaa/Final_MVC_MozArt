using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.Video
{
    public class VideoEditVM
    {
        public int Id { get; set; }

        [Required]
        public string VideoURL { get; set; }

        [Required(ErrorMessage = "Please upload an image.")]
        public IFormFile? Photo { get; set; }

        public string Image { get; set; }
    }
}
