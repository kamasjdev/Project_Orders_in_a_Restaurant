using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZamowieniaRestauracja
{
    public partial class History : UserControl
    {
        ConnectionDB_LINQDataContext dc = new ConnectionDB_LINQDataContext();
        public History()
        {
            InitializeComponent();
        }

        private void Details(object sender, EventArgs e)     // funkcja pokazująca szczegóły zamówienia
        {
            Int32 selectedCellCount = showOrders.GetCellCount(DataGridViewElementStates.Selected);  // wybrana ilość komórek
            if (selectedCellCount == 1)                         // gdy wybrana jest tylko 1 komórka
            {
                showDetailsofOrder.Visible = true;                   
                int selected_row = showOrders.SelectedCells[0].RowIndex;                 // wybrany wiersz
                DataGridViewRow selectedRow = showOrders.Rows[selected_row];             // kolekcja wszystkich komórek w wierszu
                int index_searched = (int)selectedRow.Cells[0].Value;                       // indeks wybranego wiersza

                var record = from p in dc.Produkty
                             where p.zm_id == index_searched 
                             select new { p.pr_id, p.pr_nazwa, p.pr_cena, p.zm_id }; // tworzenie encji Produkty ze ściśle określonego zapytania (wyświetlanie danych)
                showDetailsofOrder.DataSource = record;   // przekaż wyniki record (IQueryable) do showDetailsofOrder
            }
        }

        private void DeleteOrderFromDB(object sender, EventArgs e)   // funkcja pozwalająca na usunięcie rekordu z bazy danych
        {
            Int32 selectedCellCount = showOrders.GetCellCount(DataGridViewElementStates.Selected); // wybrana ilość komórek
            if (selectedCellCount > 0)           // gdy wybrano komórkę/komórki
            {
                var confirm_delete_data = MessageBox.Show("Czy chcesz usunąć danie", "Usuń danie",
                               MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);         // wyświetla okienko informacyjne o potwierdzeenie usunięcia danych

                if (confirm_delete_data == DialogResult.Yes)  // jeśli tak to
                {
                    List<int> indexes_to_delete = new List<int>();   // lista indeksów danych
                    for (int i =0; i< selectedCellCount; i++)      // pętla która dodaje indeksy do listy
                    {
                        int selected_row = showOrders.SelectedCells[i].RowIndex;
                        DataGridViewRow selectedRow = showOrders.Rows[selected_row];
                        indexes_to_delete.Add((int)selectedRow.Cells[0].Value);    // dodawanie indeksów do listy
                    }
                    
                    for (int i = 0; i < selectedCellCount; i++)            // pętla umożliwiająca usuwanie danych
                    {
                        try
                        {
                            var deleteOrder = (from zam in dc.GetTable<Zamowienia>()
                                               where zam.zm_id == indexes_to_delete[i]
                                               select zam).Single<Zamowienia>();     // tworzenie encji Zamowienia ze ściśle określonego zapytania (usuwanie rekordu), jeśli nie ma dokładnie jednego elementu w sekwencji  
                            dc.GetTable<Zamowienia>().DeleteOnSubmit(deleteOrder); // umieszcza encji (obiektu) Zamowienia w oczekiwaniu na usuwanie
                            dc.SubmitChanges();    // Oblicza zestaw zmodyfikowanych obiektów, które mają zostać usunięte, i wykonuje odpowiednie polecenia w celu zaimplementowania zmian w bazie danych
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);       // wyświetl w oknie błąd
                        }
                        
                        // odświeżanie rekordów w tabeli
                        var results = from z in dc.Zamowienia 
                                     select new { z.zm_id, z.zm_nr_zamowienia, z.zm_data_zamowienia, 
                                         z.zm_koszt, z.zm_email };      // tworzenie encji Zamówienia ze ściśle określonego zapytania (wyświetlanie danych)
                        showOrders.DataSource = results; // przekaż wyniki results (IQueryable) do showOrders
                        showDetailsofOrder.Visible = false; // schowaj tabelę showDetailsofOrder
                    }
                }
                else
                    return;
            }
        }

        private void LoadLeftHistory(object sender, EventArgs e)       // funkcja wykrywająca zmianę pozycji Visible
        {
            if (this.Visible == true) // jeśli Visible jest true to pokaż rekordy z tabeli Zamówienia
            {
                var results = from z in dc.Zamowienia select new { z.zm_id, z.zm_nr_zamowienia, z.zm_data_zamowienia, z.zm_koszt, z.zm_email };
                showOrders.DataSource = results; // przekaż wyniki results (IQueryable) do showOrders
            }
        }
    }
}
