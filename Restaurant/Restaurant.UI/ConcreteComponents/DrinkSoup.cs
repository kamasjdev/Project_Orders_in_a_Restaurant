
using Restaurant.UI.Components;

namespace Restaurant.UI.ConcreteComponents
{
    public class DrinkSoup : Product
    {
        public DrinkSoup(string name, double cost)
        {
            this.name = name;
            this.cost = cost;
        }

        public override double CalculateCost()
        {
            return cost;
        }

        public override string GetName()
        {
            return name;
        }

        public override string ToString()
        {
            return $"{this.name}, cena {this.cost}zł";
        }
    }
}
