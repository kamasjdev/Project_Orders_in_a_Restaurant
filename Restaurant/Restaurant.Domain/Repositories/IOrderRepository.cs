using Restaurant.Domain.Entities;
using System;

namespace Restaurant.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Guid, Order>
    {
        Order GetLatestOrderOnDateAsync(DateTime currentDate);
    }
}
