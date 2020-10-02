using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZamowieniaRestauracja.Components;
using ZamowieniaRestauracja.Decorator;

namespace ZamowieniaRestauracja.ConcreteDecorator
{
    public class MainCourseSalads : ProductDecorator
    {
        public MainCourseSalads(Product produkt) : base(produkt)
        {

        }

        public override double CalculateCost()
        {
            return base.CalculateCost() + 5.00;
        }

        public override string GetName()
        {
            return base.GetName() + " z sałatkami";
        }

        public override string ToString()
        {
            return $"{GetName()}, cena {CalculateCost()}zł";
        }
    }
}
