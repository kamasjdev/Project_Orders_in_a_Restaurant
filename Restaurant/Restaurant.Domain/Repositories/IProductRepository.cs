using Restaurant.Domain.Entities;
using System;

namespace Restaurant.Domain.Repositories
{
    public interface IProductRepository : IRepository<Guid, Product>
    {
    }
}
