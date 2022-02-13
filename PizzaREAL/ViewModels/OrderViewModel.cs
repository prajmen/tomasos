using PizzaREAL.Models;
using System.ComponentModel;

namespace PizzaREAL.ViewModels
{
    public class OrderViewModel
    {
        public Order Order { get; set; } = new Order();

        [DisplayName("Kontotyp")]
        public ICollection<string> UserRoles { get; set; } = new List<string>();
        public int CustomerPoints { get; set; }
        public int AccumulatedPoints { get; set; }
        public int TotalDiscount { get; set; }
        public int BonusActivationDiscount { get; set; }
        public bool IsBonusActivated { get; set; }
        public List<Dish>? Dishes { get; set; }

    }
}
