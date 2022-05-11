using Restaurant.Domain.Entities;
using System;

namespace Restaurant.ApplicationLogic.DTO
{
    public class ProductSaleDto : BaseDto
    {
        public AdditionDto Addition { get; set; }
        public ProductDto Product { get; set; }
        public decimal EndPrice { get; set; }
        public Guid? OrderId { get; set; }
        public ProductSaleState ProductSaleState { get; set; }
    }
}
