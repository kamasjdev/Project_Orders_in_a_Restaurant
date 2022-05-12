using Dapper;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Restaurant.Infrastructure.Repositories
{
    internal class AdditonRepository : IAdditonRepository

    {
        private readonly IDbConnection _dbConnection;

        public AdditonRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Guid Add(Addition entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Addition Get(Guid id)
        {
            var sql = "SELECT * FROM additions WHERE Id = @Id";
            var result = _dbConnection.Query<AdditionPOCO>(sql, new { Id = id });
            return result.SingleOrDefault()?.AsEntity();
        }

        public ICollection<Addition> GetAll()
        {
            var sql = "SELECT * FROM additions";
            var result = _dbConnection.Query<AdditionPOCO>(sql);
            return result.Select(o => o.AsEntity()).ToList();
        }

        public void Update(Addition entity)
        {
            throw new NotImplementedException();
        }
    }
}
