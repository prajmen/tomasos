using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaREAL.Models;
using PizzaREAL.Services;
using PizzaREAL.ViewModels;

namespace PizzaREAL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IngredientController : Controller
    {
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public IActionResult GetIngredients()
        {
            return View( _ingredientService.GetIngredients());
        }
        public IActionResult AddIngredient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddIngredient(Ingredient model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Ingredient = "Ingrediensen las till i databasen!";
                ModelState.Clear();
                _ingredientService.AddIngredient(model);
            }
            
            return View();
        }
              
        [HttpGet]
        public IActionResult RemoveIngredient(int id)
        {
            _ingredientService.RemoveIngredient(id);
            ViewBag.Ingredient = "Ingrediens bortagen";
            return View("GetIngredients", _ingredientService.GetIngredients());
        }


        [HttpGet]
        public IActionResult UpdateIngredient(int id)
        {            
            return View(_ingredientService.GetById(id));
        }

        [HttpPost]
        public IActionResult UpdateIngredient(Ingredient model)
        {
            _ingredientService.UpdateIngredient(model);
            ViewBag.Ingredient = "Ingrediensens namn uppdaterad!";
            return View("GetIngredients", _ingredientService.GetIngredients());
        }
    }
}
