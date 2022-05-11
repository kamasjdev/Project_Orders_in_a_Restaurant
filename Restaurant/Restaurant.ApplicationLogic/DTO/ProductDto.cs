﻿using Restaurant.Domain.Entities;

namespace Restaurant.ApplicationLogic.DTO
{
    public class ProductDto : BaseDto
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public ProductKind ProductKind { get; set; }
    }
}
