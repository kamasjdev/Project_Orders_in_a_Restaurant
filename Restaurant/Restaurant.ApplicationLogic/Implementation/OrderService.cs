using Restaurant.ApplicationLogic.DTO;
using Restaurant.ApplicationLogic.Interfaces;
using Restaurant.ApplicationLogic.Mappings;
using Restaurant.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.ApplicationLogic.Implementation
{
    internal class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Guid Add(OrderDto order)
        {
            order.Id = Guid.NewGuid();
            var id = _orderRepository.Add(order.AsEntity());
            return id;
        }

        public void Delete(Guid id)
        {
            _orderRepository.Delete(id);
        }

        public OrderDetailsDto Get(Guid id)
        {
            var order = _orderRepository.Get(id);
            return order.AsDetailsDto();
        }

        public IEnumerable<OrderDto> GetAll()
        {
            var orders = _orderRepository.GetAll();
            return orders.Select(o => o.AsDto());
        }

        public void Update(OrderDto order)
        {
            _orderRepository.Update(order.AsEntity());
        }
    }
}
