using Castle.MicroKernel.Lifestyle;
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
            using (var scope = _container.BeginScope())
            {
                var service = _container.Resolve<TService>();
                var exceptionMapper = _container.Resolve<IMapToApplicationException>();
                try
                {
                    return action.Invoke(service);
                }
                catch(Exception ex)
                {
                    var exception = exceptionMapper.Map(ex);
                    throw exception;
                }
            }
        }

        public void Send<TService>(Action<TService> action)
             where TService : class, IService
        {
            using (var scope = _container.BeginScope())
            {
                var service = _container.Resolve<TService>();
                var exceptionMapper = _container.Resolve<IMapToApplicationException>();
                try
                {
                    action(service);
                }
                catch(Exception ex)
                {
                    var exception = exceptionMapper.Map(ex);
                    throw exception;
                }
            }
        }
    }
}
