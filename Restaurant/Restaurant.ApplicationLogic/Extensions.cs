using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Restaurant.ApplicationLogic.Exceptions;
using Restaurant.ApplicationLogic.Implementation;
using Restaurant.ApplicationLogic.Interfaces;
using Restaurant.ApplicationLogic.Mail;

namespace Restaurant.ApplicationLogic
{
    public static class Extensions
    {
        public static IWindsorContainer AddApplicationLogic(this IWindsorContainer container)
        {
            container.Register(Component.For<IProductService>().ImplementedBy<ProductService>().LifestyleTransient());
            container.Register(Component.For<IOrderService>().ImplementedBy<OrderService>().LifestyleTransient());
            container.Register(Component.For<IAdditonService>().ImplementedBy<AdditonService>().LifestyleTransient());
            container.Register(Component.For<IMailSender>().ImplementedBy<MailSender>().LifestyleTransient());
            container.Register(Component.For<IOptions>().ImplementedBy<Options>().LifestyleSingleton());
            container.Register(Component.For<IProductSaleService>().ImplementedBy<ProductSaleService>().LifestyleTransient());
            container.Register(Component.For<IMapToApplicationException>().ImplementedBy<MapToApplicationException>().LifestyleSingleton());
            return container;
        }
    }
}
