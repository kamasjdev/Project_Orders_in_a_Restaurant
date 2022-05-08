using Restaurant.ApplicationLogic.DTO;
using Restaurant.ApplicationLogic.Interfaces;
using System;
using System.Collections.Generic;

namespace Restaurant.ApplicationLogic.Implementation
{
    internal class OrderService : IOrderService
    {
        public Guid Add(OrderDto order)
        {
            throw new NotImplementedException();
        }

        public void Delete(OrderDto order)
        {
            throw new NotImplementedException();
        }

        public OrderDto Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(OrderDto order)
        {
            throw new NotImplementedException();
        }
    }
}
