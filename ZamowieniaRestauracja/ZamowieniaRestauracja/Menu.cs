using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZamowieniaRestauracja.Components;
using ZamowieniaRestauracja.ConcreteComponents;
using ZamowieniaRestauracja.ConcreteDecorator;
using System.IO;
using System.Net.Mail;

namespace ZamowieniaRestauracja
{
    
    
    public partial class Menu : UserControl
    {
        Dictionary<string, double> products = new Dictionary<string, double>(); // kolekcja produktow (nazwa) i wartości (ceny)
        List<Product> list_of_products = new List<Product>();     // lista produktów
        double amount_to_pay = 0.0;             // kwota do zapłaty

        public Menu()
        {
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
            if(comboBoxMainDishes1.SelectedIndex>=0 && comboBoxMainDishes1.SelectedIndex <=3)
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
            else if(comboBoxMainDishes1.SelectedIndex == 4) // jeśli wybrano głowne danie z indeksu 4 to pokaż drugą część dania głowne np. frytki, zmieniaki ryż i dodatki
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
            else if(comboBoxMainDishes1.SelectedIndex > 4 && comboBoxMainDishes1.SelectedIndex <= 6) // jeśli wybrano głowne danie z indeksu od 5 do 6 to pokaż dodatki
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
            // jeśli wybrano głowne danie z indeksu od 0 do 3 to stwórz obiekt product i w razie czego udekoruj (dodaj dodatek)
            if (comboBoxMainDishes1.SelectedIndex >= 0 && comboBoxMainDishes1.SelectedIndex <= 3)
            {
                string name = (comboBoxMainDishes1.SelectedItem.ToString()); // przypisz nazwę obiektu z comboBoxMainDishes1
                double cost = 0.0; // ustaw koszt na 0

                try
                {
                    cost = products[name]; // wybierz z kolekcji products odpowiedni produkt i przekaż jego wartość
                }
                catch (KeyNotFoundException) // szukany klucz nie isnieje to przerwij działanie funkcji ("łapie" błąd)
                {
                    MessageBox.Show("Podane danie nie istnieje", "Wybierz danie",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);  // wyświetl komunikat
                    return;
                }

                // stwórz obiekt product o podanej nazwie (name) i kwocie (cost)
                Product product = new Pizza(name, cost);
                // jeśli został wybrany dodatek to udekoruj product
                if (comboBoxAdditions.SelectedItem != null && comboBoxAdditions.SelectedIndex > 0)
                {
                    switch(comboBoxAdditions.SelectedIndex)
                    {
                        case 1:
                            product = new PizzaDoubleCheese(product); // pizza z podwójnym serem
                            break;
                        case 2:
                            product = new PizzaSalami(product); // pizza z salami
                            break;
                        case 3:
                            product = new PizzaHam(product); // pizza z szynką
                            break;
                        case 4:
                            product = new PizzaChampignons(product); // pizza z pieczarkami
                            break;
                        default:
                            break;
                    }
                }
                list_of_products.Add(product);     // dodaj product do listy produktów
                listViewOrderedProducts.Items.Add(product.ToString()); // dodaj product do listy zamówionych produktów
            }
            // jeśli wybrano głowne danie z indeksu od 4 to stwórz obiekt product i w razie czego udekoruj (dodaj dodatek)
            else if (comboBoxMainDishes1.SelectedIndex == 4)  
            {
                string name = comboBoxMainDishes1.SelectedItem.ToString(); // przypisz nazwę obiektu (string) z comboBoxMainDishes1
                double cost = 0.0;              // ustaw koszt na 0
                comboMainDishes2.BackColor = Color.White;     // ustaw okno wyboru głownego dania na kolor biały
                if (comboMainDishes2.SelectedItem != null)   // gdy zostało wybrane pole dodatku do dania głownego (np. dla schabowego frytki, ziemniaki, ryż)
                {
                    try
                    {
                        cost = products[name];  // wybierz z kolekcji products odpowiedni produkt i przekaż jego wartość
                    }
                    catch (KeyNotFoundException)  // szukany klucz nie isnieje to przerwij działanie funkcji ("łapie" błąd)
                    {
                        MessageBox.Show("Podane danie nie istnieje", "Wybierz danie",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);  // wyświetl komunikat
                        return;
                    }
                    name += " " + comboMainDishes2.SelectedItem.ToString(); // dopisz nazwę obiektu (string) z comboBoxMainDishes2

                    Product product = new MainCourse(name, cost); // stwórz obiekt product o podanej nazwie (name) i kwocie (cost)
                    // jeśli wybrano dodatki to udekoruj product
                    if (comboBoxAdditions.SelectedItem != null && comboBoxAdditions.SelectedIndex > 0)
                    {
                        switch (comboBoxAdditions.SelectedIndex)
                        {
                            case 1:
                                product = new MainCourseSalads(product); // z sałatkami
                                break;
                            case 2:
                                product = new MainCourseSauces(product); // z sosami
                                break;
                        }
                    }

                    list_of_products.Add(product); // dodaj product do listy produktów

                    listViewOrderedProducts.Items.Add(product.ToString()); // dodaj product do listy zamówionych produktów
                    comboMainDishes2.SelectedItem = null;  // ustaw comboMainDishes2 wybrany indeks na null aby zapobiec "zapisaniu" dania następnego jako poprzednie 
                    // np. wybrano schabowy z frytkami, to po ponownym wybraniu tego samego dania jeśli wartość nie została wyzerowana zostanie dodany obiekt ten sam, czyli schabowy z frytkami
                }
                else
                {
                    MessageBox.Show("Wybierz z czym chcesz to danie", "Wybierz danie",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                    comboMainDishes2.BackColor = Color.LightBlue;       // okienko pojawia się jeśli nie wybrano dodatku do dania głównego
                }
            }
            // jeśli wybrano głowne danie z indeksu od 5 do 6 to stwórz obiekt product i w razie czego udekoruj (dodaj dodatek)
            else if (comboBoxMainDishes1.SelectedIndex > 4 && comboBoxMainDishes1.SelectedIndex <= 6) 
            {
                string name = comboBoxMainDishes1.SelectedItem.ToString();   // przypisz nazwę obiektu (string) z comboBoxMainDishes1
                double cost = 0.0;        // ustaw koszt na 0

                try
                {
                    cost = products[name]; // wybierz z kolekcji products odpowiedni produkt i przekaż jego wartość
                }
                catch (KeyNotFoundException)  // szukany klucz nie isnieje to przerwij działanie funkcji ("łapie" błąd)
                {
                    MessageBox.Show("Podane danie nie istnieje", "Wybierz danie",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);  // wyświetl komunikat
                    return;
                }

                Product product = new MainCourse(name, cost);  // stwórz obiekt product o podanej nazwie (name) i kwocie (cost)
                // jeśli wybrano dodatki to udekoruj product
                if (comboBoxAdditions.SelectedItem != null && comboBoxAdditions.SelectedIndex > 0)
                {
                    switch (comboBoxAdditions.SelectedIndex)
                    {
                        case 1:
                            product = new MainCourseSalads(product); // z sałatkami
                            break;
                        case 2:
                            product = new MainCourseSauces(product); // z sosami
                            break;
                    }
                }

                list_of_products.Add(product); // dodaj product do listy produktów
                listViewOrderedProducts.Items.Add(product.ToString()); // dodaj product do listy zamówionych produktów
            }
            else if (comboBoxMainDishes1.SelectedIndex > 6 && comboBoxMainDishes1.SelectedIndex <= 11) // jeśli wybrano głowne danie z indeksu od 7 do 11 to stwórz obiekt product
            {
                string name = comboBoxMainDishes1.SelectedItem.ToString(); // przypisz nazwę obiektu (string) z comboBoxMainDishes1
                double cost = 0.0;        // ustaw koszt na 0

                try
                {
                    cost = products[name];      // wybierz z kolekcji products odpowiedni produkt i przekaż jego wartość
                }
                catch (KeyNotFoundException)   // szukany klucz nie isnieje to przerwij działanie funkcji ("łapie" błąd)
                {
                    MessageBox.Show("Podane danie nie istnieje", "Wybierz danie",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);  // wyświetl komunikat
                    return;
                }

                Product product = new DrinkSoup(name, cost); // stwórz obiekt product o nazwie (name) i cenie (cost)
                list_of_products.Add(product); // dodaj product do listy produktów

                listViewOrderedProducts.Items.Add(product.ToString()); // dodaj product do listy zamówionych produktów
            }
            else   // jeśli wybrano indeks powyżej 11 to wyświetl komunikat że podane danie nie istnieje
            {
                MessageBox.Show("Podane danie nie istnieje", "Wybierz danie",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
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
                string email_from = "";    // zmienna określająca email nadawcy
                string email_to = "";           // zmienna określająca email odbiorcy
                string smtp_klient = "";           // smtp klient
                int smtp_port=0;             // port smtp
                string login = "";           // login 
                string pass = "";                // hasło
                string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.txt";     // ścieżka do pliku z ustawieniami
                try
                {
                    using (StreamReader sr = File.OpenText(path)) // odczytywanie z pliku
                    {
                        string s; // przykładowy string
                        int i = 1;
                        while ((s = sr.ReadLine()) != null) // odczytywanie po wierszach
                        {
                            switch (i)
                            {
                                case 1:
                                    email_from = s; // email nadawcy
                                    break;
                                case 2:
                                    email_to = s;  // email odbiorcy
                                    break;
                                case 3:
                                    smtp_klient = s; // klient smtp
                                    break;
                                case 4:
                                    smtp_port = Convert.ToInt32(s); // port smtp
                                    break;
                                case 5:
                                    login = s; // login 
                                    break;
                                case 6:
                                    pass = s; // hasło
                                    break;
                                default:
                                    break;
                            }
                            i++;
                        }
                    }
                }
                catch (FileLoadException load_exception) // błąd wczytywania pliku
                {
                    MessageBox.Show(load_exception.Message, "Błąd wczytywania pliku",
                                   MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                    return;                // przerwij działanie funkcji
                }
                catch (FileNotFoundException not_found) // nie znaleziono pliku
                {
                    MessageBox.Show(not_found.Message, "Nie znaleziono pliku",
                                   MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                    return;           // przerwij działanie funkcji
                }
                catch (IOException input_output_exception) // błąd wejścia/wyjścia
                {
                    MessageBox.Show(input_output_exception.Message, "Błąd",
                                   MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                    return;        // przerwij działanie funkcji
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Błąd",
                                   MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                    return;      // przerwij działanie funkcji
                }

                if (email_from != "" && email_to != "") // gdy email nie jest pusty
                {
                    // blok użyty aby przechwycić string który nie jest adresem email
                    try
                    {
                        MailAddress mail1 = new MailAddress(email_from);
                        MailAddress mail2 = new MailAddress(email_to);
                    }
                    catch (FormatException format_exception)
                    {
                        //messagebox
                        MessageBox.Show("Wprowadź dane w ustawieniach " + format_exception.Message, "Realizacja zamówienia",
                                       MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                        return;       // przerwij działanie funkcji
                    }
                }
                else
                {
                    MessageBox.Show("Wprowadź dane w ustawieniach", "Realizacja zamówienia",
                              MessageBoxButtons.OK,
                             MessageBoxIcon.Information);
                    return; //  przerwij działanie funkcji
                }

                // dekodowanie loginu i hasła
                byte[] data = System.Convert.FromBase64String(login);
                login = System.Text.ASCIIEncoding.ASCII.GetString(data);
                data = System.Convert.FromBase64String(pass);
                pass = System.Text.ASCIIEncoding.ASCII.GetString(data);

                Order order = new Order(list_of_products); // stwórz zamówienie (obiekt) z listy produktów
                order.Email = email_to;
                User user = new User(email_from, email_to, smtp_klient, smtp_port, login, pass); // utwórz obiekt użytkownika

                MailSender.Email(MailSender.ContentEmail(order), user, order.Get_Order_Nr()); // prześlij zamówienie na maila

                // dodawanie rekordów do tabeli 
                using (ConnectionDB_LINQDataContext db = new ConnectionDB_LINQDataContext()) //DataContext źródło wszystkich encji (Produkty, Zamowienia) mapowanych za pośrednictwem połączenia z bazą danych (connectionString)
                {
                    Zamowienia z = new Zamowienia(); // tworzenie encji zamówienia
                    z.zm_nr_zamowienia = order.Get_Order_Nr(); // przypisanie wartości nr zamówienia w tabeli SQL
                    z.zm_data_zamowienia = order.Get_Order_Date(); // przypisanie wartości daty zamówienia w tabeli SQL
                    z.zm_koszt = (float)order.CalculateCost(); // przypisanie wartości całkowitego kosztu zamówienia w tabeli SQL
                    z.zm_email = order.Email;
                    db.Zamowienia.InsertOnSubmit(z); // dodaje obiekt w oczekiwaniu na zaakceptowanie dodania rekordów

                    try
                    {
                        db.SubmitChanges();    // zatwierdź zmiany
                    }   
                    catch (Exception exception)  // wystąpi błąd wyświetl okno z zawartością błędu
                    {
                        MessageBox.Show(exception.Message, "Zamówienie",
                                       MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                    }
                    
                    // przeszukaj tabelę w celu znalezienia id zamówienia
                    var id_order = from zam in db.Zamowienia where zam.zm_nr_zamowienia == order.Get_Order_Nr() select zam;
                    
                    // dla każdego produktu przypisz wartości poszczególnym kolumną
                    foreach (Product prod in list_of_products)
                    {
                        Produkty p = new Produkty();
                        p.pr_nazwa = prod.GetName();
                        p.pr_cena = (float)prod.CalculateCost();
                        p.zm_id = id_order.First().zm_id;
                        db.Produkty.InsertOnSubmit(p);
                    }

                    try
                    {
                        db.SubmitChanges();     // zatwierdź zmiany
                    }
                    catch (Exception exception) // wystąpi błąd wyświetl okno z zawartością błędu
                    {
                        MessageBox.Show(exception.Message, "Zamówienie",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
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
