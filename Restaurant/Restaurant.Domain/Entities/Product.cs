using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Entities
{
    public class Product : BaseEntity<Guid>
    {
        private Product()
        {
            _orders = new List<Order>();
        }

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
        private IList<Order> _orders;

        public void ChangePrice(decimal price)
        {
            if (price < 0)
            {
                throw new InvalidOperationException($"Price '{price}' cannot be negative");
            }

            Price = price;
        }

        public void ChangeProductName(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                throw new InvalidOperationException("ProductName cannot be empty");
            }

            if (productName.Length < 3)
            {
                throw new InvalidOperationException("ProductName should have at least 3 characters");
            }

            ProductName = productName;
        }

        public void AddOrders(IEnumerable<Order> orders)
        {
            if (orders is null)
            {
                throw new InvalidOperationException("Cannot add empty orders");
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
                throw new InvalidOperationException("Cannot add null order");
            }

            _orders.Add(order);
        }
    }
}
