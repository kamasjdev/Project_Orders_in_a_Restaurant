using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Restaurant.Infrastructure.Mappings
{
    public class ProductPOCO
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public ProductKind ProductKind { get; set; }
        public IList<OrderPOCO> Orders { get; set; } = new List<OrderPOCO>();
    }
}
