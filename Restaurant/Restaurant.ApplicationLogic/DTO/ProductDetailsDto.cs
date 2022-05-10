using Restaurant.Domain.Entities;
using System.Collections.Generic;

namespace Restaurant.ApplicationLogic.DTO
{
    public class ProductDetailsDto : ProductDto
    {
        public IEnumerable<OrderDto> Orders { get; set; }
    }
}
