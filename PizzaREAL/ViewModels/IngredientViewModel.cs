using PizzaREAL.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PizzaREAL.ViewModels
{
    public class IngredientViewModel
    {
        public Ingredient ActiveIngredient { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
