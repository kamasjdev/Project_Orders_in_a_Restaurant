using Restaurant.ApplicationLogic.Interfaces;
using System;

namespace Restaurant.Infrastructure.Requests
{
    public interface IRequestHandler
    {
        TResponse Send<TService, TResponse>(Func<TService, TResponse> action)
            where TService : class, IService;

        void Send<TService>(Action<TService> action)
            where TService : class, IService;
    }
}
