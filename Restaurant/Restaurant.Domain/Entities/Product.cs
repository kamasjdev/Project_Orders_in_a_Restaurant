using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.Domain.Entities
{
    public class Product : BaseEntity<Guid>
    {
        private Product()
        {
            Orders = new List<Order>();
        }

        public Product(Guid id, string productName, decimal price, IList<Order> orders = null)
        {
            Id = id;
            ChangeProductName(productName);
            ChangePrice(price);
            Orders = orders ?? new List<Order>();
        }

        public string ProductName { get; private set; }
        public decimal Price { get; private set; }
        public IList<Order> Orders { get; private set; }

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
                Orders.Add(order);
            }
        }
    }
}
