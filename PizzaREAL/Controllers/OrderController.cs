using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PizzaREAL.Models;
using PizzaREAL.ModelsIdentity;
using PizzaREAL.Services;
using PizzaREAL.ViewModels;
using System.Security.Claims;

namespace PizzaREAL.Controllers
{
    [Authorize]
    //[Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IDishService _dishService;
        private readonly IAccountService _accountService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(IOrderService orderService, IDishService dishService, IAccountService accountService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _dishService = dishService;
            _accountService = accountService;
            _userManager = userManager;
        }

        public IActionResult GetMenu()
        {
            var orderViewModel = new OrderViewModel();
            orderViewModel.Dishes = _dishService.GetDishes();
            return View(orderViewModel);
        }

        public IActionResult GetOrders()
        {
            return View(_orderService.GetOrders());
        }

        public IActionResult UpdateOrderDelivered(int id)
        {
            _orderService.UpdateOrderDelivered(id);
            ViewBag.Order = "Ordern Uppdaterades!";           

            return View("GetOrders", _orderService.GetOrders());
        }

        public IActionResult UpdateOrderUnDelivered(int id)
        {
            _orderService.UpdateOrderUnDelivered(id);
            ViewBag.Order = "Ordern Uppdaterades!";

            return View("GetOrders", _orderService.GetOrders());
        }

        public IActionResult RemoveOrder(int id)
        {
            _orderService.RemoveOrder(id);

            ViewBag.Order = "Ordern bortagen!";
            return View("GetOrders", _orderService.GetOrders());
        }

        [HttpGet]
        public IActionResult AddDishToOrder(int dishId)
        {
            OrderViewModel orderViewModel = new OrderViewModel();
            string cartJSON;

            if (HttpContext.Session.GetString("cart") == null)
            {
                orderViewModel = new OrderViewModel();

                Task<ApplicationUser> task = Task.Run<ApplicationUser>(async () => await GetUser());

                orderViewModel.UserRoles = _userManager.GetRolesAsync(task.Result).Result;
                string userId = _userManager.GetUserId(HttpContext.User);

                orderViewModel.Order.Customer = _accountService.GetCustomerByUserID(userId);
            }
            else
            {
                orderViewModel = ReturnOrderViewModelFromSession();
            }

            var dish = _dishService.GetById(dishId);
            orderViewModel = _orderService.AddDishToOrderViewModel(orderViewModel, dish);

            SetOrderViewModelToSession(orderViewModel);

            return PartialView("_CartPartial", orderViewModel);
        }

        public IActionResult RemoveDishFromOrder(int dishId)
        {
            var orderViewModel = ReturnOrderViewModelFromSession();
            orderViewModel = _orderService.RemoveDishFromOrderViewModel(orderViewModel, dishId);
            SetOrderViewModelToSession(orderViewModel);

            return PartialView("_CartPartial", orderViewModel);
        }

        [HttpPost]
        public IActionResult AddOrderToContext ()
        {
            _orderService.AddOrderToContext(ReturnOrderViewModelFromSession());
            HttpContext.Session.Clear();
            var orderViewModel = new OrderViewModel();
            orderViewModel.Dishes = _dishService.GetDishes();

            ViewBag.Order = "Beställning mottagen. Var god hämta om 20 min!";

            return View("GetMenu", orderViewModel);
        }

        public IActionResult ActivateBonus()
        {
            var orderViewModel = ReturnOrderViewModelFromSession();
            orderViewModel = _orderService.ActivateBonus(orderViewModel);
            SetOrderViewModelToSession(orderViewModel);

            return PartialView("_CartPartial", orderViewModel);
        }
        public IActionResult DeactivateBonus()
        {
            var orderViewModel = ReturnOrderViewModelFromSession();
            orderViewModel = _orderService.DeactivateBonus(orderViewModel);
            SetOrderViewModelToSession(orderViewModel);

            return PartialView("_CartPartial", orderViewModel);
        }

        private OrderViewModel ReturnOrderViewModelFromSession()
        {
            string cartJSON = HttpContext.Session.GetString("cart");
            var orderViewModel = JsonConvert.DeserializeObject<OrderViewModel>(cartJSON);

            return orderViewModel;
        }

        private void SetOrderViewModelToSession(OrderViewModel orderViewModel)
        {
            string cartJSON = JsonConvert.SerializeObject(orderViewModel, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            HttpContext.Session.SetString("cart", cartJSON);
        }

        private async Task<ApplicationUser> GetUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User) as ApplicationUser;
        }
    }
}
