using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.Support
{
    public class SupportEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

    
    }
}
