using System.Net.Mail;

namespace Ducode.Essentials.Mail.Interfaces
{
   /// <summary>
   /// Describes a class that is used for sending mails.
   /// </summary>
   public interface IMailService
   {
      /// <summary>
      /// Sends an email.
      /// </summary>
      /// <param name="message">The message.</param>
      void SendMail(MailMessage message);
   }
}
