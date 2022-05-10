using System.Collections.Generic;

namespace Restaurant.ApplicationLogic.DTO
{
    public class OrderDetailsDto : OrderDto
    {
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
