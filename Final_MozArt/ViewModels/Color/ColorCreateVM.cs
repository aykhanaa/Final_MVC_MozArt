using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.Color
{
    public class ColorCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}
