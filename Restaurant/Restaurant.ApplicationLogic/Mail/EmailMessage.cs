using System;

namespace Restaurant.ApplicationLogic.Mail
{
    public sealed class EmailMessage
    {
        private readonly string _subject;
        private readonly string _body;

        public string Subject => _subject;
        public string Body => _body;

        public EmailMessage(string subject, string body)
        {
            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new InvalidOperationException("Email cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(body))
            {
                throw new InvalidOperationException("Email cannot be empty");
            }

            _subject = subject;
            _body = body;
         }
    }
}
