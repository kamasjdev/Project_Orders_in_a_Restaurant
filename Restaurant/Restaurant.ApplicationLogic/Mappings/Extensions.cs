using Restaurant.ApplicationLogic.DTO;
using Restaurant.Domain.Entities;
using System;
using System.Linq;

namespace Restaurant.ApplicationLogic.Mappings
{
    internal static class Extensions
    {
        public static Product AsEntity(this ProductDto productDto)
        {
            var product = new Product(productDto.Id, productDto.ProductName, productDto.Price, (Domain.Entities.ProductKind) productDto.ProductKind);
            return product;
        }

        public static ProductDto AsDto(this Product product)
        {
            var productDto = new ProductDto()
            {
                Id = product.Id,
                Price = product.Price,
                ProductName = product.ProductName,
                ProductKind = (DTO.ProductKind) product.ProductKind
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
                ProductKind = (DTO.ProductKind) product.ProductKind,
                Orders = product.Orders.Select(o => o.AsDto())
            };

            return productDto;
        }

        public static Order AsEntity(this OrderDto orderDto)
        {
            var order = new Order(orderDto.Id, orderDto.OrderNumber, orderDto.Created, orderDto.Price, Email.Of(orderDto.Email), orderDto.Note);
            return order;
        }

        public static OrderDto AsDto(this Order order)
        {
            var orderDto = new OrderDto()
            {
                Id = order.Id,
                Email = order.Email.Value,
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
                Email = order.Email.Value,
                OrderNumber = order.OrderNumber,
                Created = order.Created,
                Price = order.Price,
                Note = order.Note,
                Products = order.Products.Select(p => p.AsDto()).ToList()
            };

            return orderDto;
        }

        public static ProductSaleDto AsDto(this ProductSale productSale)
        {
            var productSaleDto = new ProductSaleDto()
            {
                Id = productSale.Id,
                Addition = productSale.Addition?.AsDto(),
                AdditionId = productSale.Addition?.Id,
                Email = productSale.Email.Value,
                EndPrice = productSale.EndPrice,
                OrderId = productSale.OrderId,
                Product = productSale.Product.AsDto(),
                ProductId = productSale.ProductId,
                ProductSaleState = (DTO.ProductSaleState) productSale.ProductSaleState
            };

            return productSaleDto;
        }

        public static ProductSale AsEntity(this ProductSaleDto productSale)
        {
            var productSaleDto = new ProductSale(productSale.Id, productSale.Product.AsEntity(),
                        (Domain.Entities.ProductSaleState) productSale.ProductSaleState, Email.Of(productSale.Email),
                        productSale.Addition?.AsEntity(), productSale.OrderId);

            return productSaleDto;
        }

        public static ProductSaleDetailsDto AsDetailsDto(this ProductSale productSale)
        {
            var productSaleDto = new ProductSaleDetailsDto()
            {
                Id = productSale.Id,
                Addition = productSale.Addition.AsDto(),
                AdditionId = productSale.Addition?.Id,
                Email = productSale.Email.Value,
                EndPrice = productSale.EndPrice,
                OrderId = productSale.OrderId,
                Product = productSale.Product.AsDto(),
                ProductId = productSale.ProductId,
                ProductSaleState = (DTO.ProductSaleState) productSale.ProductSaleState,
                Order = productSale.Order.AsDto()
            };

            return productSaleDto;
        }

        public static AdditionDto AsDto(this Addition addition)
        {
            var additionDto = new AdditionDto()
            {
                Id = addition.Id,
                AdditionName = addition.AdditionName,
                Price = addition.Price,
                ProductKind = (DTO.ProductKind) addition.ProductKind
            };

            return additionDto;
        }

        public static Addition AsEntity(this AdditionDto addition)
        {
            var additionDto = new Addition(addition.Id, addition.AdditionName, addition.Price, (Domain.Entities.ProductKind) addition.ProductKind);
            return additionDto;
        }
    }
}
