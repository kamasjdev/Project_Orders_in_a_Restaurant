using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Restaurant.Domain.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public const string EMAIL_PATTERN = "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";

        private Order() { _products = new List<ProductSale>(); }

        public Order(Guid id, string orderNumber, DateTime created, decimal price, string email, string note = null, IEnumerable<ProductSale> products = null) 
            : base(id)
        {
            ChangeOrderNumber(orderNumber);
            Created = created;
            ChangePrice(price);
            ChangeEmail(email);

            if (products != null)
            {
                AddProducts(products);
            }
            else
            {
                _products = new List<ProductSale>();
            }
            
            Note = note;
        }

        public string OrderNumber { get; private set; }
        public DateTime Created { get; }
        public decimal Price { get; private set; }
        public string Email { get; private set; }
        public string Note { get; set; } = null;

        public IEnumerable<ProductSale> Products => _products;
        private IList<ProductSale> _products = new List<ProductSale>();

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

        public void AddProducts(IEnumerable<ProductSale> products)
        {
            if (products is null)
            {
                throw new InvalidOperationException("Cannot add empty products");
            }

            foreach (var product in products)
            {
                AddProduct(product);
            }
        }

        public void AddProduct(ProductSale product)
        {
            if (product is null)
            {
                throw new InvalidOperationException("Cannot add null product");
            }

            var productToAdd = _products.Where(p => p.Id == product.Id).SingleOrDefault();

            if (productToAdd != null)
            {
                throw new InvalidOperationException($"Product with id '{productToAdd.Id}' exists");
            }

            _products.Add(product);
            product.AddOrder(this);
        }

        public void RemoveProduct(ProductSale product)
        {
            if (product is null)
            {
                throw new InvalidOperationException("Cannot remove null product");
            }

            var productToDelete = _products.Where(p => p.Id == product.Id).SingleOrDefault();

            if (productToDelete is null)
            {
                throw new InvalidOperationException($"Product with id '{productToDelete.Id}' not found");
            }

            _products.Remove(product);
            product.RemoveOrder();
        }
    }
}
