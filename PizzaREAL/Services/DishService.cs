using Microsoft.EntityFrameworkCore;
using PizzaREAL.Models;

namespace PizzaREAL.Services
{
    public interface IDishService
    {
        List<Dish> GetDishes();
        List<DishType> GetDishTypes();
        void AddDish(Dish model);
        void UpdateDish(Dish model);
        Dish GetById (int id);
    }
    public class DishService : IDishService
    {
        private readonly PizzaDbContext _context;

        public DishService(PizzaDbContext context)
        {
            _context = context;
        }

        public List<Dish> GetDishes()
        {
            return _context.Dishes.Include("Ingredients").ToList();
        }

        public List<DishType> GetDishTypes()
        {

            return _context.DishTypes.ToList(); 
        }

        public void AddDish(Dish model)
        {
            _context.Dishes.Add(model);
            _context.SaveChanges();
        }

        public Dish GetById(int id)
        {
            var dish = _context.Dishes.Include("Ingredients").Where(d => d.DishId == id).FirstOrDefault();
            return dish;
        }

        public void UpdateDish(Dish model)
        {
            var dish = GetById(model.DishId);
            dish.DishName = model.DishName;
            dish.Description = model.Description;
            dish.DishTypeId = model.DishTypeId;
            dish.Price = model.Price;
            dish.Ingredients = model.Ingredients;

            _context.Dishes.Update(dish);
            _context.SaveChanges();
        }
    }
}
