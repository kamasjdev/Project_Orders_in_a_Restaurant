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
            var sql = "INSERT INTO products (Id, ProductName, Price, ProductKind) VALUES (@Id, @ProductName, @Price, @ProductKind)";
            _dbConnection.Execute(sql, entity);
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
                        LEFT JOIN product_sales ps ON ps.ProductId = p.Id
                        LEFT JOIN additions a on ps.AdditionId = a.Id
                        LEFT JOIN orders o ON o.Id = ps.OrderId
                        WHERE p.Id = @Id";
            var result = _dbConnection.Query<Product, ProductSale, Addition, Order, Product>(sql,
                (product, productSale, addition, order) => { 
                    if (order?.Id != Guid.Empty) {
                        product.AddOrder(order);
                    }
                    return product; },
                new { Id = id })
                .GroupBy(o => o.Id)
                .Select(group =>
                {
                    var combinedOwner = group.First();
                    var orders = group.Select(owner => owner.Orders.SingleOrDefault()).ToList();

                    if (orders.Any(o => o is null))
                    {
                        return combinedOwner;
                    }

                    combinedOwner.AddOrders(orders);
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
            var sql = "UPDATE products SET ProductName = @ProductName, Price = @Price, ProductKind = @ProductKind WHERE Id = @Id";
            _dbConnection.Execute(sql, entity);
        }
    }
}
