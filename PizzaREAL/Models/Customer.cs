using System;
using System.Collections.Generic;

namespace PizzaREAL.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string AspNetUserId { get; set; } = null!;
        public int BonusPoints { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
