using System;
using System.Collections.Generic;

namespace PizzaREAL.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDishes = new HashSet<OrderDish>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalSum { get; set; }
        public bool Delivered { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<OrderDish> OrderDishes { get; set; }
    }
}
