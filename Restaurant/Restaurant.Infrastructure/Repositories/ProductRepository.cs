using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;

namespace Restaurant.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProductRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Guid Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public Product Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
