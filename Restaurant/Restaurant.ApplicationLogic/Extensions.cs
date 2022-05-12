using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Restaurant.ApplicationLogic.Implementation;
using Restaurant.ApplicationLogic.Interfaces;

namespace Restaurant.ApplicationLogic
{
    public static class Extensions
    {
        public static IWindsorContainer AddApplicationLogic(this IWindsorContainer container)
        {
            container.Register(Component.For<IProductService>().ImplementedBy<ProductService>().LifestyleTransient());
            container.Register(Component.For<IOrderService>().ImplementedBy<OrderService>().LifestyleTransient());
            container.Register(Component.For<IAdditonService>().ImplementedBy<AdditonService>().LifestyleTransient());
            return container;
        }
    }
}
