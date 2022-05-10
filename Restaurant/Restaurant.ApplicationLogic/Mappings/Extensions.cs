using Restaurant.ApplicationLogic.DTO;
using Restaurant.Domain.Entities;
using System.Linq;

namespace Restaurant.ApplicationLogic.Mappings
{
    internal static class Extensions
    {
        public static Product AsEntity(this ProductDto productDto)
        {
            var product = new Product()
            {
                Id = productDto.Id,
                Price = productDto.Price,
                ProductName = productDto.ProductName
            };

            return product;
        }

        public static ProductDto AsDto(this Product product)
        {
            var productDto = new ProductDto()
            {
                Id = product.Id,
                Price = product.Price,
                ProductName = product.ProductName
            };

            return productDto;
        }

        public static ProductDetailsDto AsDetailsDto(this Product product)
        {
            var productDto = new ProductDetailsDto()
            {
                Id = product.Id,
                Price = product.Price,
                ProductName = product.ProductName,
                Orders = product.Orders.Select(o => o.AsDto())
            };

            return productDto;
        }

        public static Order AsEntity(this OrderDto orderDto)
        {
            var order = new Order()
            {
                Id = orderDto.Id,
                Email = orderDto.Email,
                Price = orderDto.Price,
                OrderNumber = orderDto.OrderNumber,
                Created = orderDto.Created
            };

            return order;
        }

        public static OrderDto AsDto(this Order order)
        {
            var orderDto = new OrderDto()
            {
                Id = order.Id,
                Email = order.Email,
                OrderNumber = order.OrderNumber,
                Created = order.Created,
                Price = order.Price
            };

            return orderDto;
        }

        public static OrderDetailsDto AsDetailsDto(this Order order)
        {
            var orderDto = new OrderDetailsDto()
            {
                Id = order.Id,
                Email = order.Email,
                OrderNumber = order.OrderNumber,
                Created = order.Created,
                Price = order.Price,
                Products = order.Products.Select(p => p.AsDto())
            };

            return orderDto;
        }
    }
}
