using Castle.Windsor;
using Restaurant.ApplicationLogic.Interfaces;
using System;

namespace Restaurant.Infrastructure.Requests
{
    internal class RequestHandler : IRequestHandler
    {
        private readonly IWindsorContainer _container;

        public RequestHandler(IWindsorContainer windsorContainer)
        {
            _container = windsorContainer;
        }

        public TResponse Send<TService, TResponse>(Func<TService, TResponse> action)
             where TService : class, IService
        {
            var service = _container.Resolve<TService>();
            return action.Invoke(service);
        }

        public void Send<TService>(Action<TService> action)
             where TService : class, IService
        {
            var service = _container.Resolve<TService>();
            action(service);
        }
    }
}
