using Restaurant.UI.Components;
using Restaurant.UI.Decorator;

namespace Restaurant.UI.ConcreteDecorator
{
    public class PizzaDoubleCheese : ProductDecorator
    {
        public PizzaDoubleCheese(Product produkt) : base(produkt)
        {

        }

        public override double CalculateCost()
        {
            return base.CalculateCost() + 2.00;
        }

        public override string GetName()
        {
            return base.GetName() + " z podwójnym serem";
        }

        public override string ToString()
        {
            return $"{GetName()}, cena {CalculateCost()}zł";
        }
    }
}
