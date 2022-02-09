using PizzaREAL.Models;

namespace PizzaREAL.ViewModels
{
    public class AddDishViewModel
    {
        public Dish NewDish { get; set; }
        public List<Ingredient>? Ingredients { get; set; }
        public List<DishType>? DishTypes { get; set; }
        public int[] IngredientIds { get; set; }

    }
}
