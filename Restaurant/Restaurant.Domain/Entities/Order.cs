using Restaurant.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
            Products = new HashSet<Product>();
        }

        public string OrderNumber { get; set; }
        public DateTime Created { get; set; }
        public decimal Price { get; set; }
        public string Email { get; set; }
        public ISet<Product> Products { get; set; }
    }
}
