using Microsoft.AspNetCore.Mvc;

namespace PizzaREAL.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult GetDishes()
        {
            return View();
        }
    }
}
