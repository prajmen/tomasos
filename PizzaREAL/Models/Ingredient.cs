using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaREAL.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            Dishes = new HashSet<Dish>();
        }

        [NotMapped]
        public int IngredientId { get; set; }
        [DisplayName("Namn")]
        [StringLength(20, ErrorMessage = "Namn på ingrediens kan inte vara mer än 20 tecken")]
        public string IngredientName { get; set; } = null!;

        public virtual ICollection<Dish> Dishes { get; set; }
    }
}
