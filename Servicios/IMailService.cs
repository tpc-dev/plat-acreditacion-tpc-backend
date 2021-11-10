using PlatAcreditacionTPCBackend.Models;

namespace PlatAcreditacionTPCBackend.Servicios
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
