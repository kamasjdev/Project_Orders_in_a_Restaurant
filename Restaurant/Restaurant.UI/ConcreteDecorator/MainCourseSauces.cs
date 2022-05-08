using Restaurant.UI.Components;
using Restaurant.UI.Decorator;

namespace Restaurant.UI.ConcreteDecorator
{
    public class MainCourseSauces : ProductDecorator
    {
        public MainCourseSauces(Product produkt) : base(produkt)
        {

        }

        public override double CalculateCost()
        {
            return base.CalculateCost() + 6.00;
        }

        public override string GetName()
        {
            return base.GetName() + " z sosami";
        }

        public override string ToString()
        {
            return $"{GetName()}, cena {CalculateCost()}zł";
        }
    }
}
