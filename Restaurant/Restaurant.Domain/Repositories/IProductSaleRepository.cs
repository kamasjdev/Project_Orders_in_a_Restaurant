using Restaurant.Domain.Entities;
using System;

namespace Restaurant.Domain.Repositories
{
    public interface IProductSaleRepository : IRepository<Guid, ProductSale>
    {
    }
}
