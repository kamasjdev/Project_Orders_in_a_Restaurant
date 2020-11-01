using System;
using System.Collections.Generic;
using ZamowieniaRestauracja.Components;

namespace ZamowieniaRestauracja
{
    public class Order : Product 
    {
        private List<Product> _product; // lista produktów
        private DateTime _date_order;     // data zamówienia
        private int _nr_order;        // nr zamówienia
        public string Email { get; set; }

        public int Get_Order_Nr() => _nr_order;
        public DateTime Get_Order_Date() => _date_order;

        public int getCountOfList() => _product.Count;

        public Order(List<Product> product)
        {
            Random rnd = new Random();
            this._product = product;
            _nr_order = this.GetHashCode()+rnd.Next(1,59);
            _date_order = DateTime.Now;
        }

        public override double CalculateCost()
        {
            double kwota = 0.0;
            foreach (Product p in _product)
                kwota += p.CalculateCost();
            return kwota;
        }

        public Product getSingleProduct(int i)
        {
            return _product[i];
        }

        public override string GetName() => $"Nr Zamówienia {Get_Order_Nr()}";

        public override string ToString()
        {
            return $"{Get_Order_Nr()} {Get_Order_Date()} {CalculateCost()} {Email}";
        }
    }
}
