using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.Support
{
    public class SupportCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required] 
        public string? Description { get; set; }

    }
}
