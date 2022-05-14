﻿using Dapper;
using Restaurant.ApplicationLogic.Interfaces;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Restaurant.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbConnection _dbConnection;

        public OrderRepository(IUnitOfWork unitOfWork)
        {
            _dbConnection = unitOfWork.Connection;
        }

        public Guid Add(Order entity)
        {
            var sql = "INSERT INTO orders (Id, OrderNumber, Created, Price, Email, Note) VALUES (@Id, @OrderNumber, @Created, @Price, @Email, @Note)";
            _dbConnection.Execute(sql, entity);
            return entity.Id;
        }

        public void Delete(Guid id)
        {
            var sql = "DELETE FROM orders WHERE Id = @Id";
            _dbConnection.Execute(sql, new { Id = id });
        }

        public void DeleteWithPositions(IEnumerable<Guid> ids)
        {
            var sqlProductSales = "DELETE FROM product_sales WHERE OrderId IN @Ids";
            _dbConnection.Execute(sqlProductSales, new { Ids = ids });
            var sqlOrders = "DELETE FROM orders WHERE Id IN @Ids";
            _dbConnection.Execute(sqlOrders, new { Ids = ids });
        }

        public Order Get(Guid id)
        {
            var sql = @"SELECT * FROM orders o
                        LEFT JOIN product_sales ps ON ps.OrderId = o.Id
                        LEFT JOIN additions a on ps.AdditionId = a.Id
                        LEFT JOIN products p ON p.Id = ps.ProductId
                        WHERE o.Id = @Id";
            var result = _dbConnection.Query<OrderPOCO, ProductSalePOCO, AdditionPOCO, ProductPOCO, OrderPOCO>(sql,
                (order, productSale, addition, product) => {
                    if (productSale != null && productSale.Id != Guid.Empty)
                    {
                        if(addition != null && addition.Id != Guid.Empty)
                        {
                            productSale.Addition = addition;
                        }
                        productSale.Product = product;
                        order.Products.Add(productSale);
                    }
                    return order;
                },
                new { Id = id })
                .GroupBy(o => o.Id)
                .Select(group =>
                {
                    var combinedOwner = group.First();
                    var products = group.Select(owner => owner.Products.SingleOrDefault()).ToList();
                    
                    if (products.Any(p => p is null))
                    {
                        return combinedOwner;
                    }

                    foreach(var product in products)
                    {
                        combinedOwner.Products.Add(product);
                    }

                    combinedOwner.Products = combinedOwner.Products.Distinct().ToList();
                    
                    return combinedOwner;
                });

            var orderToReturn = result.SingleOrDefault();
            return orderToReturn?.AsDetailsEntity();
        }

        public ICollection<Order> GetAll()
        {
            var sql = "SELECT * FROM orders";
            var result = _dbConnection.Query<OrderPOCO>(sql);
            return result.Select(o => o.AsEntity()).ToList();
        }

        public Order GetLatestOrderOnDateAsync(DateTime currentDate)
        {
            var sql = @"SELECT o.* FROM orders o
                        WHERE Datetime(o.Created) > @CurrentDate
                        ORDER BY o.Created DESC
                        LIMIT 1";
            var result = _dbConnection.Query<OrderPOCO>(sql, new
            {
                CurrentDate = currentDate.Date
            }).SingleOrDefault();
            return result?.AsEntity();
        }

        public void Update(Order entity)
        {
            var sql = "UPDATE orders SET OrderNumber = @OrderNumber, Created = @Created, Price = @Price, Email = @Email WHERE Id = @Id";
            _dbConnection.Execute(sql, entity);
        }
    }
}
