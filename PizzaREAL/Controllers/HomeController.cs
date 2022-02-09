using Microsoft.AspNetCore.Mvc;
using PizzaREAL.Services;

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
            return View(_service.GetDishes());
        }
    }
}
