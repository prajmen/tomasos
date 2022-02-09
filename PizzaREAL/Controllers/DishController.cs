using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaREAL.Services;
using PizzaREAL.ViewModels;

namespace PizzaREAL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DishController : Controller
    {
        private readonly IDishService _dishService;
        private readonly IIngredientService _ingredientService;

        public DishController(IDishService dishService, IIngredientService ingredientService)
        {
            _dishService = dishService;
            _ingredientService = ingredientService;
        }

        public IActionResult GetDishes()
        {
            return View(_dishService.GetDishes());
        }

        public IActionResult AddDish()
        {

            return View(AddDishViewModel());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddDish(AddDishViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.NewDish.Ingredients = _ingredientService.GetRange(model.IngredientIds);
                _dishService.AddDish(model.NewDish);
                ViewBag.Dish = "Maträtten las till i databasen";
                ModelState.Clear();
            }

            return View(AddDishViewModel());
        }

        public IActionResult UpdateDish(int id)
        {
            var viewModel = AddDishViewModel();
            viewModel.NewDish = _dishService.GetById(id);
            
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult UpdateDish(AddDishViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.NewDish.Ingredients = _ingredientService.GetRange(viewModel.IngredientIds);
                _dishService.UpdateDish(viewModel.NewDish);

                ViewBag.Dish = "Maträtten las till i databasen";
                ModelState.Clear();
                return View("GetDishes", _dishService.GetDishes());
            }
            else
            {
                return View(viewModel);
            }
        }




        private AddDishViewModel AddDishViewModel()
        {
            AddDishViewModel addDishViewModel = new AddDishViewModel();
            addDishViewModel.Ingredients = _ingredientService.GetIngredients();
            addDishViewModel.DishTypes = _dishService.GetDishTypes();

            return addDishViewModel;
        }
    }
}
