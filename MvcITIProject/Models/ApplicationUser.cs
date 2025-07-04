using Microsoft.AspNetCore.Identity;

namespace MvcITIProject.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string? Addresses { get; set; }


    }
}
