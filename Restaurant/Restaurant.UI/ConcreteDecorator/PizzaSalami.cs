using Restaurant.UI.Components;
using Restaurant.UI.Decorator;

namespace Restaurant.UI.ConcreteDecorator
{
    public class PizzaSalami : ProductDecorator
    {
        public PizzaSalami(Product produkt) : base(produkt)
        {

        }

        public override double CalculateCost()
        {
            return base.CalculateCost() + 2.00;
        }

        public override string GetName()
        {
            return base.GetName() + " z salami";
        }

        public override string ToString()
        {
            return $"{GetName()}, cena {CalculateCost()}zł";
        }
    }
}
