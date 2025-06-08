using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.Tag
{
    public class TagEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
