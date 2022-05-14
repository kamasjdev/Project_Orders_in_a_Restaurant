using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Restaurant.Domain.Repositories
{
    public interface IProductSaleRepository : IRepository<Guid, ProductSale>
    {
        IEnumerable<ProductSale> GetAllByOrderId(Guid orderId);
    }
}
