using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.Color
{
    public class ColorEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
