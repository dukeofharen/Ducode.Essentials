using System.Net;
using System.Net.Mail;
using Ducode.Essentials.Mail.Interfaces;

namespace Ducode.Essentials.Mail
{
   /// <summary>
   /// A class that is used for sending mails.
   /// </summary>
   /// <seealso cref="Ducode.Essentials.Mail.Interfaces.IMailService" />
   public class MailService : IMailService
   {
      private readonly ISmtpSettingsProvider _smtpSettingsProvider;

      /// <summary>
      /// Initializes a new instance of the <see cref="MailService"/> class.
      /// </summary>
      /// <param name="smtpSettingsProvider">The SMTP settings provider.</param>
      public MailService(ISmtpSettingsProvider smtpSettingsProvider)
      {
         _smtpSettingsProvider = smtpSettingsProvider;
      }

      /// <summary>
      /// Sends an email.
      /// </summary>
      /// <param name="message">The message.</param>
      public void SendMail(MailMessage message)
      {
         var settings = _smtpSettingsProvider.GetSmtpSettings();
         var client = new SmtpClient
         {
            Host = settings.Host,
            UseDefaultCredentials = settings.UseDefaultCredentials,
            Credentials = new NetworkCredential(settings.Username, settings.Password),
            Port = settings.Port,
            EnableSsl = settings.EnableSsl
         };
         client.Send(message);
      }
   }
}
