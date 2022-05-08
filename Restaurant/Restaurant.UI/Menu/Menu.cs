using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Restaurant.UI.Components;
using Restaurant.Infrastructure.Requests;

namespace Restaurant.UI
{
    public partial class Menu : UserControl
    {
        private Dictionary<string, double> products = new Dictionary<string, double>(); // kolekcja produktow (nazwa) i wartości (ceny)
        private List<Product> list_of_products = new List<Product>();     // lista produktów
        double amount_to_pay = 0.0;             // kwota do zapłaty
        private readonly IRequestHandler _requestHandler;
        
        public Menu(IRequestHandler requestHandler)
        {
            _requestHandler = requestHandler;
            InitializeComponent();
            // dodanie listy produktów do kolekcji
            products.Add("Pizza Margheritta", 20.0);
            products.Add("Pizza Tosca", 22.0);
            products.Add("Pizza Vegetariana", 25.0);
            products.Add("Pizza Venecia", 25.0);
            products.Add("Schabowy z", 30.0);
            products.Add("Ryba z frytkami", 28.0);
            products.Add("Placek po węgiersku", 27.0);
            products.Add("Zupa pomidorowa", 12.0);
            products.Add("Zupa rosół", 10.0);
            products.Add("Kawa", 5.0);
            products.Add("Herbata", 5.0);
            products.Add("Cola", 5.0);
            labelCostOfOrder.Text = "Koszt: " + amount_to_pay.ToString() + "zł";
        }


        private void ChangedItem(object sender, EventArgs e) // funkcja wywołuje się gdy zostanie zmieniona wartość w głównych daniach daniach (comboBoxMainDishes1)
        {
            // jeśli wybrano głowne danie z indeksu od 0 do 3 to pokaż dodatki
            if (comboBoxMainDishes1.SelectedIndex >= 0 && comboBoxMainDishes1.SelectedIndex <= 3)
            {
                comboMainDishes2.Visible = false;
                comboBoxAdditions.Text = "";
                label3.Visible = true;
                comboBoxAdditions.Items.Clear();
                comboBoxAdditions.Items.Add("");  // jeśli przypadkiem zostanie dodany dodatek przez przypadek można wybrać puste pole
                comboBoxAdditions.Items.Add("z podwójnym serem"); // 1
                comboBoxAdditions.Items.Add("z salami");        // 2
                comboBoxAdditions.Items.Add("z szynką");            // 3
                comboBoxAdditions.Items.Add("z pieczarkami");           // 4
                comboBoxAdditions.Visible = true;
            }
            else if (comboBoxMainDishes1.SelectedIndex == 4) // jeśli wybrano głowne danie z indeksu 4 to pokaż drugą część dania głowne np. frytki, zmieniaki ryż i dodatki
            {
                comboBoxAdditions.Text = "";
                comboMainDishes2.Text = "";
                comboMainDishes2.Visible = true;
                comboBoxAdditions.Text = "";
                label3.Visible = true;
                comboBoxAdditions.Items.Clear();
                comboBoxAdditions.Items.Add("");   // jeśli przypadkiem zostanie dodany dodatek przez przypadek można wybrać puste pole
                comboBoxAdditions.Items.Add("z barem sałatkowym");   // 1
                comboBoxAdditions.Items.Add("z zestawem sosów");       // 2
                comboBoxAdditions.Visible = true;
            }
            else if (comboBoxMainDishes1.SelectedIndex > 4 && comboBoxMainDishes1.SelectedIndex <= 6) // jeśli wybrano głowne danie z indeksu od 5 do 6 to pokaż dodatki
            {
                comboMainDishes2.Visible = false;
                comboBoxAdditions.Text = "";
                label3.Visible = true;
                comboBoxAdditions.Items.Clear();
                comboBoxAdditions.Items.Add("");    // jeśli przypadkiem zostanie dodany dodatek przez przypadek można wybrać puste pole
                comboBoxAdditions.Items.Add("Bar sałatkowy");       // 1
                comboBoxAdditions.Items.Add("Zestaw sosów");        // 2
                comboBoxAdditions.Visible = true;
            }
            else
            {
                label3.Visible = false;
                comboBoxAdditions.Visible = false;
                comboMainDishes2.Visible = false;
            }
        }

        private void AddToOrder(object sender, EventArgs e) // funkcja realizująca dodanie elementu do listy zamówienia
        {
            Product product = AppService.SelectedDish(comboBoxMainDishes1, comboMainDishes2, comboBoxAdditions, products);
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
            if (this.Visible == true) // jeśli control user Menu jest widoczne to włącz timer
                timer1.Enabled = true;
            else          // jeśli control user Menu nie jest widoczne to włącz timer
                timer1.Enabled = false;
        }

        private void OrderRealization(object sender, EventArgs e)  // funkcja realizująca zamówienie, która przesyła zawartość zamówienia na adres email i wstawia wartości do tabeli SQL
        {
            if (list_of_products.Count != 0) // gdy lista produktów nie jest pusta
            {
                User user = AppService.LoadSettings();

                if (user != null)
                {
                    Order order = new Order(list_of_products)
                    {
                        Email = user.Email_to
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
    }
}
