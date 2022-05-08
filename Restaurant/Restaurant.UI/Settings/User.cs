
namespace Restaurant.UI
{
    public class User 
    {
        public string Login { get; private set; }          // login
        public string Password { get; private set; }         // hasło
        public string SMTP_Client { get; private set; }     // smtp klient
        public int SMTP_Port { get; private set; }         // smtp port
        public string Email_from { get; private set; }      // email nadawcy
        public string Email_to { get; private set; }           // email odbiorcy

        public User(string email_from, string email_to, string smtp_klient, int smtp_port, string login, string password)
        {
            Email_from = email_from;
            Email_to = email_to;
            SMTP_Client = smtp_klient;
            SMTP_Port = smtp_port;
            Login = login;
            Password = password;
        }
    }
}
