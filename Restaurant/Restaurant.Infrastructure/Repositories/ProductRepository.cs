using Dapper;
using Restaurant.ApplicationLogic.Interfaces;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Restaurant.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProductRepository(IUnitOfWork unitOfWork)
        {
            _dbConnection = unitOfWork.Connection;
        }

        public Guid Add(Product entity)
        {
            var sql = "INSERT INTO products (Id, ProductName, Price) VALUES (@Id, @ProductName, @Price)";
            _dbConnection.Execute(sql, entity);
                //new { Id = entity.Id, ProductName = entity.ProductName, WholePart = entity.Price.WholePart, FractionalPart = entity.Price.FractionalPart });
            return entity.Id;
        }

        public void Delete(Guid id)
        {
            var sql = "DELETE FROM products WHERE Id = @Id";
            _dbConnection.Execute(sql, new { Id = id });
        }

        public Product Get(Guid id)
        {
            var sql = @"SELECT * FROM products p
                        LEFT JOIN order_product op ON op.ProductId = p.Id
                        LEFT JOIN orders o ON o.Id = op.OrderId
                        WHERE p.Id = @Id";
            var result = _dbConnection.Query<Product, Order, Product>(sql,
                (product, order) => { 
                    if (order?.Id != Guid.Empty) {
                        product.Orders.Add(order);
                    }
                    return product; },
                new { Id = id })
                .GroupBy(o => o.Id)
                .Select(group =>
                {
                    var combinedOwner = group.First();
                    combinedOwner.Orders = new HashSet<Order>(group.Select(owner => owner.Orders.Single()).ToList());
                    return combinedOwner;
                });
            return result.SingleOrDefault();
        }

        public ICollection<Product> GetAll()
        {
            var sql = "SELECT * FROM products";
            var result = _dbConnection.Query<Product>(sql);
            return result.ToList();
        }

        public void Update(Product entity)
        {
            var sql = "UPDATE products SET ProductName = @ProductName, Price = @Price WHERE Id = @Id";
            _dbConnection.Execute(sql, entity);
               // new { Id = entity.Id, ProductName = entity.ProductName, WholePart = entity.Price.WholePart, FractionalPart = entity.Price.FractionalPart });
        }
    }
}
