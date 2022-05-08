using Restaurant.UI.Components;
using Restaurant.UI.Decorator;

namespace Restaurant.UI.ConcreteDecorator
{
    public class PizzaHam : ProductDecorator
    {
        public PizzaHam(Product produkt) : base(produkt)
        {

        }

        public override double CalculateCost()
        {
            return base.CalculateCost() + 2.00;
        }

        public override string GetName()
        {
            return base.GetName() + " z szynką";
        }

        public override string ToString()
        {
            return $"{GetName()}, cena {CalculateCost()}zł";
        }
    }
}
