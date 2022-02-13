using PizzaREAL.ModelsIdentity;

namespace PizzaREAL.ViewModels
{
    public class AccountViewModel
    {
        public List<ApplicationUser> StandardUsers { get; set; }
        public List<ApplicationUser> PremiumUsers { get; set; }
        public List<ApplicationUser> AdminUsers { get; set; }
    }
}
