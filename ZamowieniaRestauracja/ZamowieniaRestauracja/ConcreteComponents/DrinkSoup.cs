using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZamowieniaRestauracja.Components;

namespace ZamowieniaRestauracja.ConcreteComponents
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
