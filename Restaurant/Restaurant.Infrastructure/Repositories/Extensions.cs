using Restaurant.Domain.Entities;
using Restaurant.Infrastructure.Mappings;
using System.Linq;

namespace Restaurant.Infrastructure.Repositories
{
    internal static class Extensions
    {
        public static Product AsEntity(this ProductPOCO productPOCO)
        {
            var product = new Product(productPOCO.Id, productPOCO.ProductName, productPOCO.Price,
                productPOCO.ProductKind);
            return product;
        }

        public static Product AsDetailsEntity(this ProductPOCO productPOCO)
        {
            var orders = productPOCO.Orders.Select(o => o.AsDetailsEntity()).ToList();
            var product = new Product(productPOCO.Id, productPOCO.ProductName, productPOCO.Price,
                   productPOCO.ProductKind, orders);
            return product;
        }

        public static Order AsEntity(this OrderPOCO orderPOCO)
        {
            var order = new Order(orderPOCO.Id, orderPOCO.OrderNumber, orderPOCO.Created, orderPOCO.Price,
                    Email.Of(orderPOCO.Email), orderPOCO.Note);
            return order;
        }

        public static Order AsDetailsEntity(this OrderPOCO orderPOCO)
        {
            var productSales = orderPOCO.Products.Select(p => p.AsEntity()).ToList();
            var order = new Order(orderPOCO.Id, orderPOCO.OrderNumber, orderPOCO.Created, orderPOCO.Price,
                    Email.Of(orderPOCO.Email), orderPOCO.Note, productSales);
            return order;
        }

        public static ProductSale AsEntity(this ProductSalePOCO productSalePOCO)
        {
            var productSale = new ProductSale(productSalePOCO.Id, productSalePOCO.Product.AsEntity(),
                productSalePOCO.ProductSaleState, Email.Of(productSalePOCO.Email), productSalePOCO.Addition?.AsEntity(),
                productSalePOCO.OrderId);
            return productSale;
        }

        public static ProductSale AsDetailsEntity(this ProductSalePOCO productSalePOCO)
        {
            var productSale = new ProductSale(productSalePOCO.Id, productSalePOCO.Product.AsEntity(),
                productSalePOCO.ProductSaleState, Email.Of(productSalePOCO.Email), productSalePOCO.Addition?.AsEntity(),
                productSalePOCO.OrderId, productSalePOCO.Order.AsEntity());
            return productSale;
        }

        public static Addition AsEntity(this AdditionPOCO additionPOCO)
        {
            var addition = new Addition(additionPOCO.Id, additionPOCO.AdditionName, additionPOCO.Price, additionPOCO.ProductKind);
            return addition;
        }
    }
}
