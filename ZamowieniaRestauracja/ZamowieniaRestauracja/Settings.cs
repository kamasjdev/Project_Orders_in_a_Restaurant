using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
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
            string email_from = ""; // email odbiorcy
            string email_to = "";  // email nadawcy
            string smtp_client = ""; // klient smtp
            int smtp_port;  // port smtp
            string login = ""; // login
            string pass = ""; // hasło
            try
            {
                email_from = textBoxEmailFrom.Text;  // przypisz adres nadawcy z textBoxEmailFrom
                email_to = textBoxEmailTo.Text;   // przypisz adres odbiorcy z textBoxEmailTo
                if (email_from != "" && email_to != "") // gdy maile nie będą puste
                {
                    MailAddress mail1 = new MailAddress(email_from); // utwórz obiekt mail1, aby wykryć czy podano poprawny adres email
                    MailAddress mail2 = new MailAddress(email_to); // utwórz obiekt mail2, aby wykryć czy podano poprawny adres email
                }
                else
                {
                    MessageBox.Show("Wypełnij polę adresu email", "Adres email",
                              MessageBoxButtons.OK,
                             MessageBoxIcon.Information);
                }

            }
            catch (FormatException format_exception) // email jest niepoprawny to wyświetl komunikat i przerwij działanie funkcji
            {
                //messagebox
                MessageBox.Show("Wpisz poprawnie adres email " + format_exception.Message, "Adres email",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                return;
            }
            catch(Exception ex)             // gdy wystąpi błąd przerwij działanie funkcji
            {
                MessageBox.Show(ex.Message, "Adres email",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                return;
            }

            smtp_client = textBoxSMTPClient.Text;    // przypisz wartość z textBoxSMTPClient
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
            
            login = textBoxLogin.Text;  // przypisz wartość z textBoxLogin
            pass = textBoxPass.Text;  // przypisz wartość z textBoxPass
            
            // enkodowanie loginu i hasła
            var byte_string = Encoding.UTF8.GetBytes(login);  // zmienna typu byte
            login = Convert.ToBase64String(byte_string);
            byte_string = Encoding.UTF8.GetBytes(pass);  // zmienna typu byte
            pass = Convert.ToBase64String(byte_string);

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
