using Restaurant.Domain.Entities;
using System;

namespace Restaurant.Infrastructure.Mappings
{
    public class AdditionPOCO
    {
        public Guid Id { get; set; }
        public string AdditionName { get; set; }
        public decimal Price { get; set; }
        public ProductKind AdditionKind { get; set; }
    }
}
