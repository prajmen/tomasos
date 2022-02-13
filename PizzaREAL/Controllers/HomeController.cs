using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaREAL.Services;
using PizzaREAL.ViewModels;
using Microsoft.AspNetCore.Authentication;

namespace PizzaREAL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDishService _service;

        public HomeController(IDishService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("GetMenu", "Order");
            }
            else
            {
                var viewModel = new IndexViewModel()
                {
                    UserLogin = new UserLoginRequest(),
                    Dishes = _service.GetDishes(),
                };

                return View(viewModel);
            }
            
        }
    }
}
