using System;
using System.Collections.Generic;

namespace PizzaREAL.Models
{
    public partial class DishType
    {
        public DishType()
        {
            Dishes = new HashSet<Dish>();
        }

        public int DishTypeId { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<Dish> Dishes { get; set; }
    }
}
