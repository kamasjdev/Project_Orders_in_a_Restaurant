using System.Collections.Generic;
using Restaurant.ApplicationLogic.DTO;
using System;

namespace Restaurant.ApplicationLogic.Interfaces
{
    public interface IOrderService
    {
        OrderDto Get(Guid id);
        IEnumerable<OrderDto> GetAll();
        Guid Add(OrderDto order);
        void Update(OrderDto order);
        void Delete(OrderDto order);
    }
}
