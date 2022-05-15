using Restaurant.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.Domain.Entities
{
    public class Product : BaseEntity<Guid>
    {
        private Product() {}

        public Product(Guid id, string productName, decimal price, ProductKind productKind, IList<Order> orders = null)
            : base(id)
        {
            ChangeProductName(productName);
            ChangePrice(price);
            _orders = orders ?? new List<Order>();
            ProductKind = productKind;
        }

        public string ProductName { get; private set; }
        public decimal Price { get; private set; }
        public ProductKind ProductKind { get; set; }

        public IEnumerable<Order> Orders => _orders;
        private IList<Order> _orders = new List<Order>();

        public void ChangePrice(decimal price)
        {
            if (price < 0)
            {
                throw new RestaurantException($"Price '{price}' cannot be negative", typeof(Product).FullName, "Price");
            }

            Price = price;
        }

        public void ChangeProductName(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                throw new RestaurantException("ProductName cannot be empty", typeof(Product).FullName, "ProductName");
            }

            if (productName.Length < 3)
            {
                throw new RestaurantException("ProductName should have at least 3 characters", typeof(Product).FullName, "ProductName");
            }

            ProductName = productName;
        }

        public void AddOrders(IEnumerable<Order> orders)
        {
            if (orders is null)
            {
                throw new RestaurantException("Cannot add empty orders", typeof(Product).FullName, "AddOrders");
            }

            foreach (var order in orders)
            {
                _orders.Add(order);
            }
        }

        public void AddOrder(Order order)
        {
            if (order is null)
            {
                throw new RestaurantException("Cannot add null order", typeof(Product).FullName, "AddOrder");
            }

            var orderToAdd = _orders.Where(p => p.Id == order.Id).SingleOrDefault();

            if (orderToAdd != null)
            {
                throw new RestaurantException($"Order with id '{orderToAdd.Id}' exists", typeof(Product).FullName, "AddOrder");
            }

            _orders.Add(order);
        }
    }
}
