using Restaurant.ApplicationLogic.Mail;
using System.Threading.Tasks;

namespace Restaurant.ApplicationLogic.Interfaces
{
    public interface IMailSender : IService
    {
        Task SendAsync(Email email, EmailMessage emailMessage);
    }
}
