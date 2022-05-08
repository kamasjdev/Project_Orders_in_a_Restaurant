using Restaurant.Domain.ValueObjects;
using System;

namespace Restaurant.Domain.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public string OrderNumber { get; set; }
        public DateTime Created { get; set; }
        public Price Price { get; set; }
        public string Email { get; set; }
        public Product Product { get; set; }
    }
}
