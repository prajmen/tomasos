using Microsoft.AspNetCore.Identity;

namespace PizzaREAL.ModelsIdentity
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PostCode { get; set; }
        public string? City { get; set; }
    }
}
