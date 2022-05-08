using Restaurant.UI.Components;

namespace Restaurant.UI.Decorator
{
    public class ProductDecorator : Product
    {
        protected Product _produkt;
        public ProductDecorator(Product produkt)
        {
            _produkt = produkt;
        }

        public override double CalculateCost()
        {
            return _produkt.CalculateCost();
        }

        public override string GetName()
        {
            return _produkt.GetName();
        }

        public override string ToString()
        {
            return $"{_produkt.GetName()}, cena {_produkt.CalculateCost()}zł";
        }
    }
}
