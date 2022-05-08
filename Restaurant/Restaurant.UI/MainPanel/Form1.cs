using System;
using System.Windows.Forms;

namespace Restaurant.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            menu1.Visible = false;   // ustaw widoczność menu1 na false
            settings1.Visible = true;  // ustaw widoczność settings1 na true
            history1.Visible = false;    // ustaw widoczność history1 na false
        }

        private void ShowMenu(object sender, EventArgs e)           // funkcja wyświetlająca Menu
        {
            settings1.Visible = false;       // ustaw widoczność settings1 na false
            history1.Visible = false;           // ustaw widoczność history1 na false
            menu1.Visible = true;                    // ustaw widoczność menu1 na true
            menu1.BringToFront();
        }

        private void ShowSettings(object sender, EventArgs e)        // funkcja wyświetlająca ustawienia
        {
            menu1.Visible = false;                // ustaw widoczność menu1 na false
            history1.Visible = false;               // ustaw widoczność history1 na false
            settings1.Visible = true;                  // ustaw widoczność settings1 na true
            settings1.BringToFront();
        }

        private void ShowHistory(object sender, EventArgs e)  // funkcja wyświetlająca Historię
        {
            menu1.Visible = false;                // ustaw widoczność menu1 na false
            settings1.Visible = false;                 // ustaw widoczność settings1 na false
            history1.Visible = true;                      // ustaw widoczność history1 na true
            history1.BringToFront();
        }

        private void Close(object sender, EventArgs e)       // funkcja zamykająca aplikację
        {
            this.Dispose();           // zamknij aplikację
        }
    }
}
