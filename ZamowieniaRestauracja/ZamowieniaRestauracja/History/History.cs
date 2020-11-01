using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ZamowieniaRestauracja
{
    public partial class History : UserControl
    {
        private ConnectionDB_LINQDataContext dc = new ConnectionDB_LINQDataContext();
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

                var record = AppService.ShowRecordsInProdDatabase(dc, index_searched);

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

                    AppService.DeleteNRecords(dc, indexes_to_delete);

                    // odświeżanie rekordów w tabeli
                    var results = AppService.ShowRecordsInZamDatabase(dc);
                    showOrders.DataSource = results; // przekaż wyniki results (IQueryable) do showOrders
                    showDetailsofOrder.Visible = false; // schowaj tabelę showDetailsofOrder
                }
                else
                    return;
            }
        }

        private void LoadLeftHistory(object sender, EventArgs e)       // funkcja wykrywająca zmianę pozycji Visible
        {
            if (this.Visible == true) // jeśli Visible jest true to pokaż rekordy z tabeli Zamówienia
            {
                var results = AppService.ShowRecordsInZamDatabase(dc);
                showOrders.DataSource = results; // przekaż wyniki results (IQueryable) do showOrders
            }
        }
    }
}
