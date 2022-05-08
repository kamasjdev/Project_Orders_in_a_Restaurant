using Restaurant.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public class Product : BaseEntity<Guid>
    {
        public string ProductName { get; set; }
        public Price Price { get; set; }
        public ISet<Order> Orders { get; set; }
    }
}
