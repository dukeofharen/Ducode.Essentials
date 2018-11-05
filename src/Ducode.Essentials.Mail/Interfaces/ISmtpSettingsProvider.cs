using Ducode.Essentials.Mail.Models;

namespace Ducode.Essentials.Mail.Interfaces
{
   /// <summary>
   /// Describes a class that is used for retrieving SMTP settings.
   /// </summary>
   public interface ISmtpSettingsProvider
   {
      /// <summary>
      /// Gets the SMTP settings.
      /// </summary>
      /// <returns>The <see cref="SmtpSettingsModel"/>.</returns>
      SmtpSettingsModel GetSmtpSettings();
   }
}
