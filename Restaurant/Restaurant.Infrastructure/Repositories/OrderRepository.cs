using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using System;
using System.Collections.Generic;

namespace Restaurant.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public Guid Add(Order entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Order entity)
        {
            throw new NotImplementedException();
        }

        public Order Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
