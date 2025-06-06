using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.Advantage
{
    public class AdvantageEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
