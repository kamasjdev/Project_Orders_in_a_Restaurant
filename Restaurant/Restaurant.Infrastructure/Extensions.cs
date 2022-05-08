using Castle.Windsor;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Repositories;
using Castle.MicroKernel.Registration;

namespace Restaurant.Infrastructure
{
    public static class Extensions
    {
        public static IWindsorContainer AddInfrastructure(this IWindsorContainer container)
        {
            container.Register(Component.For<IOrderRepository>().ImplementedBy<OrderRepository>());
            container.Register(Component.For<IProductRepository>().ImplementedBy<ProductRepository>());
            return container;
        }
    }
}
