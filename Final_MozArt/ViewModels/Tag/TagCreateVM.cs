using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.Tag
{
    public class TagCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
