using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Restaurant.Domain.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public const string EMAIL_PATTERN = "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";

        private Order() { Products = new List<Product>(); }

        public Order(Guid id, string orderNumber, DateTime created, decimal price, string email, IList<Product> products = null)
        {
            Id = id;
            ChangeOrderNumber(orderNumber);
            Created = created;
            ChangePrice(price);
            ChangeEmail(email);
            Products = products ?? new List<Product>();
        }

        public string OrderNumber { get; private set; }
        public DateTime Created { get; }
        public decimal Price { get; private set; }
        public string Email { get; private set; }
        public IList<Product> Products { get; }

        public void ChangeOrderNumber(string orderNumber)
        {
            if (string.IsNullOrWhiteSpace(orderNumber))
            {
                throw new InvalidOperationException("OrderNumber cannot be empty");
            }

            if (orderNumber.Length < 3)
            {
                throw new InvalidOperationException("OrderNumber should have at least 3 characters");
            }

            OrderNumber = orderNumber;
        }

        public void ChangePrice(decimal price)
        {
            if (price < 0)
            {
                throw new InvalidOperationException($"Price '{price}' cannot be negative");
            }

            Price = price;
        }

        public void ChangeEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new InvalidOperationException("Email cannot be empty");
            }

            if (!Regex.Match(email, EMAIL_PATTERN).Success)
            {
                throw new InvalidOperationException("Invalid Email");
            }

            Email = email;
        }

        public void AddProducts(IEnumerable<Product> products)
        {
            if (products is null)
            {
                throw new InvalidOperationException("Cannot add empty products");
            }

            if (products.Count() == 0)
            {
                return;
            }

            foreach (var product in products)
            {
                Products.Add(product);
            }
        }

    }
}
