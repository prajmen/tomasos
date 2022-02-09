using Microsoft.EntityFrameworkCore;
using PizzaREAL.Models;
using PizzaREAL.ViewModels;

namespace PizzaREAL.Services
{
    public interface IIngredientService
    {
        List<Ingredient> GetIngredients();
        void AddIngredient(Ingredient model);
        void RemoveIngredient(int id);
        void UpdateIngredient(Ingredient model);
        Ingredient GetById(int id);
        List<Ingredient> GetRange(int[] ids);
    }
    public class IngredientService : IIngredientService
    {
        private readonly PizzaDbContext _context;

        public IngredientService(PizzaDbContext context)
        {
            _context = context;
        }

        public void AddIngredient(Ingredient model)
        {
            _context.Ingredients.Add(model);
            _context.SaveChanges();
        }

        public List<Ingredient> GetIngredients()
        {

            return _context.Ingredients.Include("Dishes").ToList();
        }

        public void RemoveIngredient(int id)
        {
            var ingredient = GetById(id); 
            
            _context.Ingredients.Remove(ingredient);
            _context.SaveChanges();
        }

        public void UpdateIngredient(Ingredient model)
        {
            var ingredient = GetById(model.IngredientId);
            ingredient.IngredientName = model.IngredientName;

            _context.Ingredients.Update(ingredient);
            _context.SaveChanges();
        }

        public Ingredient GetById(int id)
        {
            var ingredient = _context.Ingredients.Where(i => i.IngredientId == id).FirstOrDefault();
            return ingredient;
        }

        public List<Ingredient> GetRange(int[] ids)
        {
            var ingredients = new List<Ingredient>();

            foreach (int id in ids)
            {
                ingredients.Add(GetById(id));
            }
            return ingredients;
        }
    }
}
