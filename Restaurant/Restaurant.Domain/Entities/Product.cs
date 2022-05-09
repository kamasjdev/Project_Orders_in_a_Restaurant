using Restaurant.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public class Product : BaseEntity<Guid>
    {
        public Product()
        {
            Orders = new HashSet<Order>();
        }

        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public ISet<Order> Orders { get; set; }
    }
}
