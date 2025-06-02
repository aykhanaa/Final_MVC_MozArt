using Microsoft.AspNetCore.Identity;

namespace Final_MozArt.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
