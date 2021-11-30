using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;
using PlatAcreditacionTPCBackend.Models;

namespace PlatAcreditacionTPCBackend.Servicios
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);

            if (mailRequest.ToEmailList.Length > 0)
            {
                InternetAddressList list = new InternetAddressList();
                foreach (var emailTo in mailRequest.ToEmailList)
                {
                    list.Add(new MailboxAddress(emailTo));
                }
                email.To.AddRange(list);
            }
            else
            {
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            }
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
            //using (StreamReader SourceReader = System.IO.File.OpenText("TemplatesHTML\\NuevoUsuarioInvitacionEmail.html"))
            //{
            //    builder.HtmlBody = SourceReader.ReadToEnd();
            //    email.Body = builder.ToMessageBody();
            //    using var smtp = new SmtpClient();
            //    smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            //    smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            //    await smtp.SendAsync(email);
            //    smtp.Disconnect(true);
            //}
           
        }
    }
}
