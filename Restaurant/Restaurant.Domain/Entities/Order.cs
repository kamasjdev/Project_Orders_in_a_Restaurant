using Restaurant.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.Domain.Entities
{
    public class Order : BaseEntity<Guid>
    {
        private Order() {}

        public Order(Guid id, string orderNumber, DateTime created, decimal price, Email email, string note = null, IEnumerable<ProductSale> products = null) 
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
            
            Note = note;
        }

        public string OrderNumber { get; private set; }
        public DateTime Created { get; }
        public decimal Price { get; private set; }
        public Email Email { get; private set; }
        public string Note { get; set; } = null;

        public IEnumerable<ProductSale> Products => _products;
        private IList<ProductSale> _products = new List<ProductSale>();

        public void ChangeOrderNumber(string orderNumber)
        {
            if (string.IsNullOrWhiteSpace(orderNumber))
            {
                throw new RestaurantException("OrderNumber cannot be empty", typeof(Order).FullName, "OrderNumber");
            }

            if (orderNumber.Length < 3)
            {
                throw new RestaurantException("OrderNumber should have at least 3 characters", typeof(Order).FullName, "OrderNumber");
            }

            OrderNumber = orderNumber;
        }

        public void ChangePrice(decimal price)
        {
            if (price < 0)
            {
                throw new RestaurantException($"Price '{price}' cannot be negative", typeof(Order).FullName, "Price");
            }

            Price = price;
        }

        public void ChangeEmail(Email email)
        {
            Email = email;
        }

        public void AddProducts(IEnumerable<ProductSale> products)
        {
            if (products is null)
            {
                throw new RestaurantException("Cannot add empty products", typeof(Order).FullName, "AddProducts");
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
                throw new RestaurantException("Cannot add null product", typeof(Order).FullName, "AddProduct");
            }

            var productToAdd = _products.Where(p => p.Id == product.Id).SingleOrDefault();

            if (productToAdd != null)
            {
                throw new RestaurantException($"Product with id '{productToAdd.Id}' exists", typeof(Order).FullName, "AddProduct");
            }

            _products.Add(product);
            product.AddOrder(this);
        }

        public void RemoveProduct(ProductSale product)
        {
            if (product is null)
            {
                throw new RestaurantException("Cannot remove null product", typeof(Order).FullName, "RemoveProduct");
            }

            var productToDelete = _products.Where(p => p.Id == product.Id).SingleOrDefault();

            if (productToDelete is null)
            {
                throw new RestaurantException($"Product with id '{productToDelete.Id}' not found", typeof(Order).FullName, "RemoveProduct");
            }

            _products.Remove(product);
            product.RemoveOrder();
        }
    }
}
