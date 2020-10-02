using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZamowieniaRestauracja.Components
{
    public abstract class Product
    {
        protected double cost;          // cena
        protected string name;              // nazwa

        public abstract double CalculateCost(); // koszt całkowity
        public abstract string GetName(); // nazwa produktu

    }
}
