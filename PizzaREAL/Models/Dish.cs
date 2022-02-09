using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PizzaREAL.Models
{
    public partial class Dish
    {
        public Dish()
        {
            OrderDishes = new HashSet<OrderDish>();
            Ingredients = new HashSet<Ingredient>();
        }

        public int DishId { get; set; }

        [DisplayName("Namn")]
        [StringLength(20, ErrorMessage = "Användarnamn kan inte vara mer än 50 tecken")]
        [Required(ErrorMessage = "Namn är obligatoriskt")]
        public string DishName { get; set; } = null!;

        [DisplayName("Beskrivning")]
        [StringLength(200, ErrorMessage = "Beskrivning kan inte vara mer än 200 tecken")]
        [Required(ErrorMessage = "Beskrivning är obligatoriskt")]
        public string Description { get; set; }

        [DisplayName("Pris")]
        [Required(ErrorMessage = "Pris är obligatoriskt")]
        public int Price { get; set; }

        [DisplayName("Type")]
        [Required(ErrorMessage = "Typ är obligatoriskt")]
        public int DishTypeId { get; set; }

        public virtual DishType? DishType { get; set; } = null!;
        public virtual ICollection<OrderDish> OrderDishes { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
