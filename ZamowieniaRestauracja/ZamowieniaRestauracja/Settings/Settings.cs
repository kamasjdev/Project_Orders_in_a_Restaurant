using System;
using System.Windows.Forms;
using System.IO;

namespace ZamowieniaRestauracja
{
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void SetValues(object sender, EventArgs e)
        {
            int smtp_port;  // port smtp
            string email_from;
            string email_to;
            email_from = textBoxEmailFrom.Text;  // przypisz adres nadawcy z textBoxEmailFrom
            email_to = textBoxEmailTo.Text;   // przypisz adres odbiorcy z textBoxEmailTo
  
            if (email_from == "" && email_to == "" || !AppService.CheckEmail(email_from) || !AppService.CheckEmail(email_to))
            {
                    MessageBox.Show("Wypełnij polę adresu email", "Adres email",
                              MessageBoxButtons.OK,
                             MessageBoxIcon.Information);
                return;
            }

            string smtp_client = textBoxSMTPClient.Text;
            try
            {
                smtp_port = Convert.ToInt32(textBoxSTMPPort.Text);     // przypisz wartość z textBoxSTMPPort
            }
            catch (FormatException format_exception) // błąd formatu przerwij działanie funkcji
            {
                //messagebox
                MessageBox.Show("Wpisz poprawnie port SMTP "  + format_exception.Message, "Port SMTP",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                return;
            }
            catch (ArgumentNullException argument_null_exception) // textBoxSTMPPort.Text jest puste przerwij działanie funkcji
            {
                //messagebox
                MessageBox.Show("Wypełnij polę portu SMTP " + argument_null_exception.Message, "Port SMTP",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                return;
            }

            if (textBoxLogin.Text == "" || textBoxPass.Text == "")
            {
                MessageBox.Show("Wypełnij pola login i hasło", "Dane logowania",
                              MessageBoxButtons.OK,
                             MessageBoxIcon.Information);
                textBoxPass.Text = "";
                return;
            }

            // enkodowanie loginu i hasła
            string login =  AppService.EncodeDecodeString(textBoxLogin.Text, "encode");
            string pass = AppService.EncodeDecodeString(textBoxPass.Text, "encode");

            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.txt";  // ścieżka do pliku settings.txt
            using (StreamWriter sw = File.CreateText(path)) // tworzenie i zapisywanie wartości do pliku settings.txt
            {
                sw.WriteLine(email_from);  // email nadawcy 
                sw.WriteLine(email_to);    // email odbiorcy
                sw.WriteLine(smtp_client);  // smtp klient
                sw.WriteLine(smtp_port);   // smtp port
                sw.WriteLine(login);      // login
                sw.WriteLine(pass);      // hasło
            }

            MessageBox.Show("Pomyślnie ustawiono dane", "Ustawienia",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
        }
    }
}
