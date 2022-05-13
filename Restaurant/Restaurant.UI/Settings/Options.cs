using System;
using System.Text.RegularExpressions;

namespace Restaurant.UI
{
    public sealed class Options 
    {
        private const string EMAIL_PATTERN = "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
        private string _email;

        public string Login { get; set; }
        public string Password { get; set; }
        public string SmtpClient { get; set; }
        public int SmtpPort { get; set; }
        public string Email { 
            get
            {
                return _email;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidOperationException("Email cannot be empty");
                }

                if (!Regex.Match(value, EMAIL_PATTERN).Success)
                {
                    throw new InvalidOperationException("Invalid Email");
                }

                _email = value;
            }
        }

        public bool IsEmpty()
        {
            var isEmptyLogin = string.IsNullOrWhiteSpace(Login);

            if(isEmptyLogin)
                return true;

            var isEmptyPassword = string.IsNullOrWhiteSpace(Password);

            if (isEmptyPassword)
                return true;

            var isEmptySmtpClient = string.IsNullOrWhiteSpace(SmtpClient);

            if (isEmptySmtpClient)
                return true;

            var isEmptySmtpPort = SmtpPort == 0;

            if (isEmptySmtpPort)
                return true;

            var isEmptyEmail = string.IsNullOrWhiteSpace(Email);

            if (isEmptyEmail)
                return true;

            return false;
        }
    }
}
