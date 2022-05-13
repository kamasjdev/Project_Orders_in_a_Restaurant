using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Restaurant.UI.Components;
using Restaurant.Infrastructure.Requests;
using Restaurant.ApplicationLogic.Interfaces;
using Restaurant.ApplicationLogic.DTO;
using System.Linq;

namespace Restaurant.UI
{
    public partial class Menu : UserControl
    {
        private Dictionary<string, double> products = new Dictionary<string, double>(); // kolekcja produktow (nazwa) i wartości (ceny)
        private List<Product> list_of_products = new List<Product>();     // lista produktów
        double amount_to_pay = 0.0;             // kwota do zapłaty
        private readonly IRequestHandler _requestHandler;
        private IEnumerable<ProductDto> _products = new List<ProductDto>();
        private IEnumerable<AdditionDto> _additions = new List<AdditionDto>();
        private ProductDto currentProduct;
        private AdditionDto currentAddition;

        public Menu(IRequestHandler requestHandler)
        {
            _requestHandler = requestHandler;
            InitializeComponent();
        }

        private void ChangedItem(object sender, EventArgs e) // funkcja wywołuje się gdy zostanie zmieniona wartość w głównych daniach daniach (comboBoxMainDishes1)
        {
            var currentProductName = (string) comboBoxMainDishes1.SelectedItem;

            if (currentProductName is null)
            {
                return;
            }

            currentProduct = _products.Where(p => p.ProductName == currentProductName).SingleOrDefault();

            if (currentProduct is null)
            {
                return;
            }

            currentAddition = null;
            var additions = _additions.Where(a => a.ProductKind == currentProduct.ProductKind).ToList();

            if (additions.Any())
            {
                comboBoxAdditions.Text = "";
                label3.Visible = true;
                comboBoxAdditions.Items.Clear();
                comboBoxAdditions.Items.AddRange(additions.Select(a => a.AdditionName).ToArray());
                comboBoxAdditions.Visible = true;
            }
            else
            {
                label3.Visible = false;
                comboBoxAdditions.Visible = false;
            }
        }

        private void AddToOrder(object sender, EventArgs e) // funkcja realizująca dodanie elementu do listy zamówienia
        {
            Product product = AppService.SelectedDish(comboBoxMainDishes1, null, comboBoxAdditions, products);
            if (product != null)
            {
                list_of_products.Add(product);     // dodaj product do listy produktów
                listViewOrderedProducts.Items.Add(product.ToString()); // dodaj product do listy zamówionych produktów
            }
        }

        private void DeleteFromOrder(object sender, EventArgs e)  // funkcja realizująca usunięcie elementu z listy zamówienia
        {
            if (listViewOrderedProducts.SelectedItems != null) // jeśli wybrane produkty są różne od null
            {
                if (listViewOrderedProducts.SelectedIndices.Count <= 0) // gdy nie zaznaczono nic na liście zamówionych produktów to przerwij działanie funkcji
                {
                    return;
                }
                else if (listViewOrderedProducts.SelectedIndices.Count >= 1) // gdy wybrano 1 lub więcej produktów to usuń podaną ilość
                {
                    var wynik = MessageBox.Show("Czy chcesz usunąć danie", "Usuń danie",
                               MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);    // potwierdzenie usunięcia produktów

                    if (wynik == DialogResult.Yes)  // jeśli tak to usuń
                    {
                        // pętla usuwające odpowiednie produkty (umieszczone na indeksie w liście listViewOrderedProducts.SelectedIndices)
                        for (int i = listViewOrderedProducts.SelectedIndices.Count - 1; i >= 0; i--)
                        {
                            int selectedindex = listViewOrderedProducts.SelectedIndices[i]; // wybrany indeks
                            listViewOrderedProducts.Items.RemoveAt(selectedindex);  // usuń z listy listViewOrderedProducts
                            list_of_products.RemoveAt(selectedindex); // usuń z listy produktów
                        }
                    }
                    else  // w innym przypadku przerwij działanie funkcji
                        return;
                }
            }
        }

        private void RefreshCost(object sender, EventArgs e) // funkcja wywołana przez timer1 (interwał co 1s)
        {
            if (currentProduct != null)
            {
                var additionPrice = currentAddition != null ? currentAddition.Price : decimal.Zero;
                var amountPrice = currentProduct.Price + additionPrice;
                PriceProduct.Text = $"{string.Format("{0:0.00}", amountPrice)} zł";

                if (amountPrice > 0)
                {
                    PriceProduct.Visible = true;
                    PriceProductLabel.Visible = true;
                }
                else
                {
                    PriceProduct.Visible = false;
                    PriceProductLabel.Visible = false;
                }
            }
            

            amount_to_pay = 0.0; // koszt całkowity
            if (list_of_products != null) // jeśli lista produktów nie jest pusta
            {
                // obliczanie kosztów zamówienia
                foreach (Product p in list_of_products)
                    amount_to_pay = amount_to_pay + p.CalculateCost();
            }
            else // w przeciwnym wypadku ustaw całkowity koszt na 0
                amount_to_pay = 0.0;

            labelCostOfOrder.Text = "Koszt: " + amount_to_pay.ToString() + "zł"; // przypisz do labelCostOfOrder koszta
        }

        private void LoadLeftMenu(object sender, EventArgs e) // funkcja wykrywająca zmianę pozycji Visible
        {
            if (this.Visible == true)
            {
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
            }
        }

        private void OrderRealization(object sender, EventArgs e)  // funkcja realizująca zamówienie, która przesyła zawartość zamówienia na adres email i wstawia wartości do tabeli SQL
        {
            if (list_of_products.Count != 0) // gdy lista produktów nie jest pusta
            {
                Options user = AppService.LoadSettings();

                if (user != null)
                {
                    Order order = new Order(list_of_products)
                    {
                       // Email = user.Email_to
                    };// stwórz zamówienie (obiekt) z listy produktów
                    

                    bool email_sent = MailSender.Email(MailSender.ContentEmail(order), user, order.Get_Order_Nr()); // prześlij zamówienie na maila

                    if(email_sent)
                    {
                        // dodawanie rekordów do tabeli 
                        using (ConnectionDB_LINQDataContext db = new ConnectionDB_LINQDataContext()) //DataContext źródło wszystkich encji (Produkty, Zamowienia) mapowanych za pośrednictwem połączenia z bazą danych (connectionString)
                        {
                            AppService.InsertDataToZamDatabase(db, order);

                            // przeszukaj tabelę w celu znalezienia id zamówienia
                            var id_order = AppService.SearchForIdInZamDatabase(db, order.Get_Order_Nr());

                            // dla każdego produktu przypisz wartości poszczególnym kolumną               
                            AppService.InsertDataToProdDatabase(db, list_of_products, id_order);
                        }
                    }
                }
            }
            else // gdy lista produktów jest pusta to wyświetl okno
            {
                MessageBox.Show("Dodaj coś do listy", "Zamówienie",
                                   MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            labelCostOfOrder.Text = amount_to_pay > 0 ? "Koszt: " + amount_to_pay + "zł" : "";
            _products = _requestHandler.Send<IProductService, IEnumerable<ProductDto>>(s => s.GetAll());
            _additions = _requestHandler.Send<IAdditonService, IEnumerable<AdditionDto>>(s => s.GetAll());
            comboBoxMainDishes1.Items.AddRange(_products.Select(p => p.ProductName).ToArray());
        }

        private void ChangedAddition(object sender, EventArgs e)
        {
            var currentAdditionName = (string) comboBoxAdditions.SelectedItem;

            if (currentAdditionName is null)
            {
                return;
            }

            currentAddition = _additions.Where(a => a.AdditionName == currentAdditionName).SingleOrDefault();
        }
    }
}
