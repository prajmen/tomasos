using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaREAL.Models;
using PizzaREAL.ModelsIdentity;

namespace PizzaREAL.Controllers
{
    [Authorize]
    //[Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PizzaDbContext _context;

        public OrderController(UserManager<ApplicationUser> userManager, PizzaDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> CreateOrder()
        {
            int id = await ReturnLoggedInCustomerId();

            List<OrderDish> orderDishes = new List<OrderDish>();
            orderDishes.Add(new OrderDish { DishId = 1, Amount = 1 });


            Order order = new Order()
            {
                OrderDate = DateTime.Now,
                CustomerId = id,
                OrderDishes = orderDishes,
                TotalSum = 70,
            };

            
            _context.Orders.Add(order);
            _context.SaveChanges();

            //var user = await _userManager.GetUserAsync(HttpContext.User);
            //var id =  _userManager.GetUserIdAsync(user).Result;
            return View();
        }

        private async Task<int> ReturnLoggedInCustomerId()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var id = _userManager.GetUserIdAsync(user).Result;
            var customer = _context.Customers.Where(c => c.AspNetUserId == id).SingleOrDefault();

            return customer.CustomerId;
        }

        
    }
}
