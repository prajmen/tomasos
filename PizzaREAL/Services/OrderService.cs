using Microsoft.EntityFrameworkCore;
using PizzaREAL.Models;
using PizzaREAL.ViewModels;

namespace PizzaREAL.Services
{
    public interface IOrderService
    {
        OrderViewModel AddDishToOrderViewModel(OrderViewModel orderViewModel, Dish dish);
        OrderViewModel RemoveDishFromOrderViewModel(OrderViewModel orderViewModel, int dishId);
        OrderViewModel ActivateBonus(OrderViewModel orderViewModel);
        OrderViewModel DeactivateBonus(OrderViewModel orderViewModel);
        void AddOrderToContext(OrderViewModel orderViewModel);
        List<Order> GetOrders();
        void UpdateOrderDelivered(int id);
        void UpdateOrderUnDelivered(int id);
        void RemoveOrder(int id);
    }
    public class OrderService : IOrderService
    {
        private OrderViewModel _orderViewModel;
        private readonly PizzaDbContext _context;

        public OrderService(PizzaDbContext context)
        {
            _context = context;
        }

        public OrderViewModel AddDishToOrderViewModel(OrderViewModel orderViewModel, Dish dish)
        {
            _orderViewModel = orderViewModel;
            SetDishInOrder(dish);
            CalculateTotalSum(IsUserPremium());
            return _orderViewModel;
        }

        public OrderViewModel RemoveDishFromOrderViewModel(OrderViewModel orderViewModel, int dishId)
        {
            _orderViewModel = orderViewModel;
            if (!IsDishAlreadyInOrderRemove(dishId))
            {
                foreach (var orderDish in _orderViewModel.Order.OrderDishes)
                {
                    if (orderDish.DishId == dishId)
                    {
                        _orderViewModel.Order.OrderDishes.Remove(orderDish);
                    }
                }
            }
            CalculateTotalSum(IsUserPremium());
            return _orderViewModel;
        }

        private void SetDishInOrder(Dish dish)
        {
            if (!IsDishAlreadyInOrderAdd(dish.DishId))
            {
                OrderDish orderDish = new OrderDish();
                orderDish.DishId = dish.DishId;
                orderDish.Dish = dish;
                orderDish.Amount = 1;
                _orderViewModel.Order.OrderDishes.Add(orderDish);
            }

        }

        private bool IsDishAlreadyInOrderAdd(int incomingDishId)
        {
            foreach (var orderDish in _orderViewModel.Order.OrderDishes)
            {
                if (orderDish.DishId == incomingDishId)
                {
                    orderDish.Amount++;
                    return true;
                }
            }

            return false;
        }

        private bool IsDishAlreadyInOrderRemove(int incomingDishId)
        {
            foreach (var orderDish in _orderViewModel.Order.OrderDishes)
            {
                if (orderDish.DishId == incomingDishId)
                {
                    if (orderDish.Amount > 1)
                    {
                        orderDish.Amount--;
                        return true;
                    }

                }
            }

            return false;
        }

        private bool IsUserPremium()
        {
            foreach (var role in _orderViewModel.UserRoles)
            {
                if (role.Equals("Premium"))
                {
                    return true;
                }
            }
            return false;
        }



        private void CalculateTotalSum(bool premium)
        {
            int totalSum = 0;
            int totalDishes = 0;

            foreach (var orderDish in _orderViewModel.Order.OrderDishes)
            {
                totalSum += orderDish.Dish.Price * orderDish.Amount;
                totalDishes += orderDish.Amount;
            }

            if (premium)
            {
                if (totalDishes >= 3)
                {
                    _orderViewModel.Order.TotalSum = Convert.ToInt32(totalSum * 0.80);
                    _orderViewModel.TotalDiscount = totalSum - _orderViewModel.Order.TotalSum;
                }
                else
                {
                    _orderViewModel.TotalDiscount = 0;
                    _orderViewModel.Order.TotalSum = totalSum;
                }

                if (_orderViewModel.IsBonusActivated)
                {
                    _orderViewModel.BonusActivationDiscount = _orderViewModel.Order.OrderDishes.Min(od => od.Dish.Price);
                    _orderViewModel.Order.TotalSum -= _orderViewModel.BonusActivationDiscount;
                }
            }
            else
            {
                _orderViewModel.Order.TotalSum = totalSum;
            }
        }

        public OrderViewModel ActivateBonus(OrderViewModel orderViewModel)
        {
            _orderViewModel = orderViewModel;
            orderViewModel.IsBonusActivated = true;
            orderViewModel.Order.Customer.BonusPoints -= 100;
            CalculateTotalSum(IsUserPremium());
            return orderViewModel;

        }

        public OrderViewModel DeactivateBonus(OrderViewModel orderViewModel)
        {
            _orderViewModel = orderViewModel;
            orderViewModel.IsBonusActivated = false;
            orderViewModel.Order.Customer.BonusPoints += 100;
            CalculateTotalSum(IsUserPremium());
            return orderViewModel;
        }

        public void AddOrderToContext(OrderViewModel orderViewModel)
        {
            _orderViewModel = orderViewModel;

            var order = new Order()
            {
                OrderDate = DateTime.Now,
                CustomerId = _orderViewModel.Order.Customer.CustomerId,
                TotalSum = _orderViewModel.Order.TotalSum,
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var orderDish in _orderViewModel.Order.OrderDishes)
            {
                var newOrderDish = new OrderDish()
                {
                    Amount = orderDish.Amount,
                    DishId = orderDish.DishId,
                    OrderId = order.OrderId,     
                };
                _context.OrderDishes.Add(newOrderDish);
                _context.SaveChanges();
            }

            if (IsUserPremium())
            {
                var customer = _context.Customers.Where(c => c.CustomerId == _orderViewModel.Order.Customer.CustomerId).SingleOrDefault();
                
                foreach (var orderDish in _orderViewModel.Order.OrderDishes)
                {
                    customer.BonusPoints += orderDish.Amount * 10;
                }                
                
                _context.SaveChanges();                                
            }
        }

        public List<Order> GetOrders()
        { 
            //query.Include(x => x.Collection.Select(y => y.Property))
            var orders = _context.Orders.Include(o => o.OrderDishes).ThenInclude(od => od.Dish).ToList();
            return orders;
        }

        public void UpdateOrderDelivered(int id)
        {
            var order = _context.Orders.Where(o => o.OrderId == id).FirstOrDefault();
            order.Delivered = true;
            _context.SaveChanges();

        }

        public void UpdateOrderUnDelivered(int id)
        {
            var order = _context.Orders.Where(o => o.OrderId == id).FirstOrDefault();
            order.Delivered = false;
            _context.SaveChanges();

        }

        public void RemoveOrder(int id)
        {
            var orderDishes = _context.OrderDishes.Where(od => od.OrderId == id).ToList();

            foreach (var orderDish in orderDishes)
            {
                _context.OrderDishes.Remove(orderDish);
            }

            var order = _context.Orders.Where(o => o.OrderId == id).SingleOrDefault();
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
    }
}

