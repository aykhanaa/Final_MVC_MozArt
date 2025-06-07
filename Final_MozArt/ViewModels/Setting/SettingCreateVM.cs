
using System.ComponentModel.DataAnnotations;

namespace Final_MozArt.ViewModels.Setting
{
    public class SettingCreateVM
    {
        [Required]
        public string Key { get; set; }

        [Required]
        public string? Value { get; set; }

        public IFormFile? Photo { get; set; }
    }
}
