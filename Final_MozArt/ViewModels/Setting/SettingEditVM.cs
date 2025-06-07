using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Final_MozArt.ViewModels.Setting
{
    public class SettingEditVM
    {
        public int Id { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string? Value { get; set; }

        public IFormFile? Photo { get; set; }

        public string? Image { get; set; }
    }
}
