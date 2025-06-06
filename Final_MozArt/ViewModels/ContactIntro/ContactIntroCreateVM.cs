using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.ContactIntro
{
    public class ContactIntroCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string? Description { get; set; }
    }
}
