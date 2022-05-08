using Castle.Windsor;

namespace Restaurant.Infrastructure
{
    public static class Extensions
    {
        public static IWindsorContainer AddInfrastructure(this IWindsorContainer container)
        {
            return container;
        }
    }
}
