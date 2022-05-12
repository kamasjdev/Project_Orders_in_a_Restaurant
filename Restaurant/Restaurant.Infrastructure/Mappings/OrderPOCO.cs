using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Restaurant.Infrastructure.Mappings
{ 
    public class OrderPOCO
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime Created { get; set; }
        public decimal Price { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public IList<ProductSalePOCO> Products { get; set; } = new List<ProductSalePOCO>();
    }
}
