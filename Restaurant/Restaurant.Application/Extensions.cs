using Castle.Windsor;

namespace Restaurant.ApplicationLogic
{
    public static class Extensions
    {
        public static IWindsorContainer AddApplicationLogic(this IWindsorContainer container)
        {
            return container;
        }
    }
}
