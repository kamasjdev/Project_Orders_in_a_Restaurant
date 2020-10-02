using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZamowieniaRestauracja
{
    public static class MailSender // klasa utworzona w celu umożliwienia wysłania wiadomości email
    {
        public static string ContentEmail(Order order)  // tworzy zawartość wiadomości na podstawie obiektu Zamowienia
        {
            try
            { 
                string messageBody = "<font>Nr zamówienia: " + order.Get_Order_Nr() + ", data zamówienia: " 
                    + order.Get_Order_Date() + "</font><br><br>"; // wiadomość html
                if (order == null) return messageBody = "<font>Brak numeru zamówienia</font><br><br>";
                string startTable = "<table style=\"border-collapse:collapse; text-align:center;\" >"; // początek tabeli, wyrównaj tekst do środka
                string endTabel = "</table>";
                string startHeaderRow = "<tr style=\"background-color:#6FA1D2; color:#ffffff;\">"; // kolor niebieski ciemny, tekst biały
                string endHeaderRow = "</tr>";
                string startTr = "<tr style=\"color:#555555;\">"; // kolor szary
                string endTr = "</tr>";
                string startTd = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">"; // kolor niebieski ciemny, szerokość obramowania cienka, przestrzeń wokół elementu 5px
                string endTd = "</td>";
                messageBody += startTable;
                messageBody += startHeaderRow;
                messageBody += startTd + "Nazwa Dania" + endTd;
                messageBody += startTd + "Koszt" + endTd;
                messageBody += endHeaderRow;
                // Pętla dla wszystkich produktów z zamówienia
                for (int i = 0; i <= order.getCountOfList() - 1; i++)
                {
                    messageBody = messageBody + startTr;
                    messageBody = messageBody + startTd + order.getSingleProduct(i).GetName() + endTd; //dodawanie nazwy produktu
                    messageBody = messageBody + startTd + order.getSingleProduct(i).CalculateCost() + " zł" + endTd; //dodawanie ceny produktu
                    messageBody = messageBody + endTr;
                }
                messageBody = messageBody + endTabel;
                messageBody = messageBody + "\n<font>Koszt : " + order.CalculateCost() + " zł" +  "</font>";
                return messageBody; // zwraca tabelę w HTML jako string
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Wysyłanie Email",
                              MessageBoxButtons.OK,
                             MessageBoxIcon.Information);
                return null;
            }
        }
      
        public static void Email(string message, User user, int id_order)   // funkcja odpowiada przesłanie wiadomości od nadawcy do odbiorcy
        {
            var mail = new MailMessage();   // utwórz obiekt mail klasy MailMessage
            var smtp = new SmtpClient(user.SMTP_Client, user.SMTP_Port); // utwórz obiekt smtp klasy SmtpClient
            mail.From = new MailAddress(user.Email_from);  // mail nadawcy
            mail.To.Add(user.Email_to);             // mail odbiorcy
            mail.Subject = $"Zamówienie {id_order}";            // Temat wiadomości
            mail.Body = message;                        // Treść wiadomości
            smtp.Timeout = 10000;             // timeout dla smtp 10s
            smtp.UseDefaultCredentials = false;         
            smtp.Credentials = new NetworkCredential(user.Login, user.Password);   // podanie danych do maila
            smtp.EnableSsl = true;          
            try
            {
                smtp.SendMailAsync(mail);  // wysłanie wiadomości asynchronicznie
            }
            catch (SmtpException smtport)
            {
                MessageBox.Show(smtport.Message, "Wysyłanie Email",
                              MessageBoxButtons.OK,
                             MessageBoxIcon.Information);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Wysyłanie Email",
                              MessageBoxButtons.OK,
                             MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show("Wysłano zamówienie na adres email", "Wysyłanie Email",
                              MessageBoxButtons.OK,
                             MessageBoxIcon.Information);
        }
    }
}
