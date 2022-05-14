﻿using System.Collections.Generic;

namespace Restaurant.ApplicationLogic.DTO
{
    public class OrderDetailsDto : OrderDto
    {
        public IList<ProductSaleDto> Products { get; set; } = new List<ProductSaleDto>();
    }
}
