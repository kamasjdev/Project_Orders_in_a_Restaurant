using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using Restaurant.UI.Components;
using Restaurant.UI.ConcreteComponents;
using Restaurant.UI.ConcreteDecorator;

namespace Restaurant.UI
{
    public static class AppService
    {
        // kod dla Forms Menu
        #region Menu
        public static Product SelectedDish(System.Windows.Forms.ComboBox comboBoxMainDishes1, System.Windows.Forms.ComboBox comboMainDishes2, System.Windows.Forms.ComboBox comboBoxAdditions, Dictionary<string, double> products)
        {
            // jeśli wybrano głowne danie z indeksu od 0 do 3 to stwórz obiekt product i w razie czego udekoruj (dodaj dodatek)
            if (comboBoxMainDishes1.SelectedIndex >= 0 && comboBoxMainDishes1.SelectedIndex <= 3)
            {
                string name = (comboBoxMainDishes1.SelectedItem.ToString()); // przypisz nazwę obiektu z comboBoxMainDishes1
                double cost;
                try
                {
                    cost = products[name]; // wybierz z kolekcji products odpowiedni produkt i przekaż jego wartość
                }
                catch (KeyNotFoundException) // szukany klucz nie isnieje to przerwij działanie funkcji ("łapie" błąd)
                {
                    MessageBox.Show("Podane danie nie istnieje", "Wybierz danie",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);  // wyświetl komunikat
                    return null;
                }

                // stwórz obiekt product o podanej nazwie (name) i kwocie (cost)
                Product product = new Pizza(name, cost);
                // jeśli został wybrany dodatek to udekoruj product
                if (comboBoxAdditions.SelectedItem != null && comboBoxMainDishes1.SelectedIndex > 0)
                {
                    switch (comboBoxAdditions.SelectedIndex)
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

                return product;
            }
            // jeśli wybrano głowne danie z indeksu od 4 to stwórz obiekt product i w razie czego udekoruj (dodaj dodatek)
            else if (comboBoxMainDishes1.SelectedIndex == 4)
            {
                StringBuilder name = new StringBuilder(comboBoxMainDishes1.SelectedItem.ToString()); // przypisz nazwę obiektu (string) z comboBoxMainDishes1
                comboMainDishes2.BackColor = Color.White;     // ustaw okno wyboru głownego dania na kolor biały
                if (comboMainDishes2.SelectedItem != null)   // gdy zostało wybrane pole dodatku do dania głownego (np. dla schabowego frytki, ziemniaki, ryż)
                {
                    double cost;
                    try
                    {
                        cost = products[name.ToString()];  // wybierz z kolekcji products odpowiedni produkt i przekaż jego wartość
                    }
                    catch (KeyNotFoundException)  // szukany klucz nie isnieje to przerwij działanie funkcji ("łapie" błąd)
                    {
                        MessageBox.Show("Podane danie nie istnieje", "Wybierz danie",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);  // wyświetl komunikat
                        return null;
                    }
                    name.Append(" " + comboMainDishes2.SelectedItem.ToString()); // dopisz nazwę obiektu (string) z comboBoxMainDishes2

                    Product product = new MainCourse(name.ToString(), cost); // stwórz obiekt product o podanej nazwie (name) i kwocie (cost)
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
                    comboMainDishes2.SelectedItem = null;  // ustaw comboMainDishes2 wybrany indeks na null aby zapobiec "zapisaniu" dania następnego jako poprzednie 

                    return product;
                }
                else
                {
                    MessageBox.Show("Wybierz z czym chcesz to danie", "Wybierz danie",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                    comboMainDishes2.BackColor = Color.LightBlue;       // okienko pojawia się jeśli nie wybrano dodatku do dania głównego
                    return null;
                }
            }
            // jeśli wybrano głowne danie z indeksu od 5 do 6 to stwórz obiekt product i w razie czego udekoruj (dodaj dodatek)
            else if (comboBoxMainDishes1.SelectedIndex > 4 && comboBoxMainDishes1.SelectedIndex <= 6)
            {
                string name = comboBoxMainDishes1.SelectedItem.ToString();   // przypisz nazwę obiektu (string) z comboBoxMainDishes1
                double cost;
                try
                {
                    cost = products[name]; // wybierz z kolekcji products odpowiedni produkt i przekaż jego wartość
                }
                catch (KeyNotFoundException)  // szukany klucz nie isnieje to przerwij działanie funkcji ("łapie" błąd)
                {
                    MessageBox.Show("Podane danie nie istnieje", "Wybierz danie",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);  // wyświetl komunikat
                    return null;
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

                return product;
            }
            else if (comboBoxMainDishes1.SelectedIndex > 6 && comboBoxMainDishes1.SelectedIndex <= 11) // jeśli wybrano głowne danie z indeksu od 7 do 11 to stwórz obiekt product
            {
                string name = comboBoxMainDishes1.SelectedItem.ToString(); // przypisz nazwę obiektu (string) z comboBoxMainDishes1
                double cost;
                try
                {
                    cost = products[name];      // wybierz z kolekcji products odpowiedni produkt i przekaż jego wartość
                }
                catch (KeyNotFoundException)   // szukany klucz nie isnieje to przerwij działanie funkcji ("łapie" błąd)
                {
                    MessageBox.Show("Podane danie nie istnieje", "Wybierz danie",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);  // wyświetl komunikat
                    return null;
                }

                Product product = new DrinkSoup(name, cost); // stwórz obiekt product o nazwie (name) i cenie (cost)
                return product;
            }
            else   // jeśli wybrano indeks powyżej 11 to wyświetl komunikat że podane danie nie istnieje
            {
                MessageBox.Show("Podane danie nie istnieje", "Wybierz danie",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                return null;
            }
        }

        public static Options LoadSettings()
        {
            string email_from = "";    // zmienna określająca email nadawcy
            string email_to = "";           // zmienna określająca email odbiorcy
            string smtp_klient = "";           // smtp klient
            int smtp_port = 0;             // port smtp
            StringBuilder login = new StringBuilder("");           // login 
            StringBuilder pass = new StringBuilder("");                // hasło
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
                                login.Append(s); // login 
                                break;
                            case 6:
                                pass.Append(s); // hasło
                                break;
                            default:
                                break;
                        }

                        if (s == "")
                        {
                            MessageBox.Show("Wprowadź dane w ustawieniach", "Realizacja zamówienia",
                                              MessageBoxButtons.OK,
                                             MessageBoxIcon.Information);
                            return null;
                        }
                        i++;
                    }
                    if (!CheckEmail(email_from) || !CheckEmail(email_to))
                        return null;
                }
            }
            catch (FileLoadException load_exception) // błąd wczytywania pliku
            {
                MessageBox.Show(load_exception.Message, "Błąd wczytywania pliku",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                return null;                // przerwij działanie funkcji
            }
            catch (FileNotFoundException not_found) // nie znaleziono pliku
            {
                MessageBox.Show(not_found.Message, "Nie znaleziono pliku",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                return null;           // przerwij działanie funkcji
            }
            catch (IOException input_output_exception) // błąd wejścia/wyjścia
            {
                MessageBox.Show(input_output_exception.Message, "Błąd",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                return null;        // przerwij działanie funkcji
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                return null;      // przerwij działanie funkcji
            }

            login.Replace(login.ToString(), EncodeDecodeString(login.ToString(), "decode"));
            pass.Replace(pass.ToString(), EncodeDecodeString(pass.ToString(), "decode"));

            return new Options(); // utwórz obiekt użytkownika;
        }
        #endregion

        // kod uzywany do dodawania usuwania i edycji danych w bazie 
        #region CRD_DB

        public static int SearchForIdInZamDatabase(ConnectionDB_LINQDataContext table, int id)
        {
            var id_order = from zam in table.Zamowienia where zam.zm_nr_zamowienia == id select zam; // przeszukaj tabelę w celu znalezienia id zamówienia
            return id_order.First().zm_id;
        }

        public static void InsertDataToZamDatabase(ConnectionDB_LINQDataContext db, Order order)
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
        }

        public static void InsertDataToProdDatabase(ConnectionDB_LINQDataContext db, List<Product> list_of_products, int id_order)
        {
            // przeszukaj tabelę w celu znalezienia id zamówienia
            foreach (Product prod in list_of_products)
            {
                Produkty p = new Produkty();
                p.pr_nazwa = prod.GetName();
                p.pr_cena = (float)prod.CalculateCost();
                p.zm_id = id_order;
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

        public static IQueryable ShowRecordsInZamDatabase(ConnectionDB_LINQDataContext db)
        {
            var results = from z in db.Zamowienia select new { z.zm_id, z.zm_nr_zamowienia, z.zm_data_zamowienia, z.zm_koszt, z.zm_email };
            return results;
        }

        public static IQueryable ShowRecordsInProdDatabase(ConnectionDB_LINQDataContext db, int zam_index)
        {
            var results = from p in db.Produkty
                          where p.zm_id == zam_index
                          select new { p.pr_id, p.pr_nazwa, p.pr_cena, p.zm_id }; // tworzenie encji Produkty ze ściśle określonego zapytania (wyświetlanie danych);
            return results;
        }

        public static void DeleteNRecords(ConnectionDB_LINQDataContext db, List<int> indexes_to_delete)
        {
            for (int i = 0; i < indexes_to_delete.Count; i++)            // pętla umożliwiająca usuwanie danych
            {
                try
                {
                    var deleteOrder = (from zam in db.GetTable<Zamowienia>()
                                       where zam.zm_id == indexes_to_delete[i]
                                       select zam).Single<Zamowienia>();     // tworzenie encji Zamowienia ze ściśle określonego zapytania (usuwanie rekordu), jeśli nie ma dokładnie jednego elementu w sekwencji  
                    db.GetTable<Zamowienia>().DeleteOnSubmit(deleteOrder); // umieszcza encji (obiektu) Zamowienia w oczekiwaniu na usuwanie
                    db.SubmitChanges();    // Oblicza zestaw zmodyfikowanych obiektów, które mają zostać usunięte, i wykonuje odpowiednie polecenia w celu zaimplementowania zmian w bazie danych
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);       // wyświetl w oknie błąd
                }
            }
        }

        #endregion

        // sprawdzenie poprawnosci emaila i kodowanie/dekodowanie stringa
        #region VeryfyEmail_EncodeDecodeString
        public static bool CheckEmail(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email); // utwórz obiekt mail, aby wykryć czy podano poprawny adres email
            }
            catch (FormatException format_exception) // email jest niepoprawny to wyświetl komunikat i przerwij działanie funkcji
            {
                //messagebox
                MessageBox.Show("Wpisz poprawnie adres email " + format_exception.Message, "Adres email",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                return false;
            }
            catch (Exception ex)             // gdy wystąpi błąd przerwij działanie funkcji
            {
                MessageBox.Show(ex.Message, "Adres email",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        public static string EncodeDecodeString(string text, string option)
        {
            if (option == "decode")
            {
                // rozszyfrowanie
                byte[] data = System.Convert.FromBase64String(text);
                return System.Text.ASCIIEncoding.ASCII.GetString(data);
            }
            else if (option == "encode")
            {
                // enkodowanie 
                var byte_string = Encoding.UTF8.GetBytes(text);
                return Convert.ToBase64String(byte_string);
            }
            else
                return text;
        }

        #endregion
    }
}
