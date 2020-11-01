

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
