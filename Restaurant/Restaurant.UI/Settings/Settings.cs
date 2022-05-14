using Restaurant.ApplicationLogic.Interfaces;
using System;
using System.Windows.Forms;

namespace Restaurant.UI
{
    public partial class Settings : UserControl
    {
        private readonly IOptions _options;
        private string field;
        public delegate void OptionsSaved(IOptions options);

        public static event OptionsSaved SaveSettings;

        public Settings(IOptions options)
        {
            InitializeComponent();
            _options = options;
        }

        private void SetValues(object sender, EventArgs e)
        {
            try
            {
                field = "Email";
                _options.Email = textBoxEmail.Text;
                field = "SmtpClient";
                _options.SmtpClient = textBoxSMTPClient.Text;
                field = "SmtpPort";
                _options.SmtpPort = Convert.ToInt32(textBoxSTMPPort.Text);
                field = "Login";
                _options.Login = textBoxLogin.Text;
                field = "Password";
                _options.Password = textBoxPass.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Niepoprawne dane " + ex.Message, field,
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                return;
            }

            try
            {
                _options.SaveOptions();
            }
            catch
            {
                MessageBox.Show("Wystąpił błąd podczas zapisu", "Save",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Pomyślnie ustawiono dane", "Ustawienia",
                               MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
            
            SaveSettings(_options);
        }
    }
}
