using Restaurant.Domain.Entities;
using System;

namespace Restaurant.Infrastructure.Mappings
{
    public class ProductSalePOCO
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public ProductPOCO Product { get; set; }
        public Guid? AdditionId { get; set; }
        public AdditionPOCO Addition { get; set; }
        public decimal EndPrice { get; set; }
        public Guid? OrderId { get; set; }
        public OrderPOCO Order { get; set; }
        public ProductSaleState ProductSaleState { get; set; }
        public string Email { get; set; }
    }
}
