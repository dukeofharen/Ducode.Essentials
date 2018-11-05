using Ducode.Essentials.Mail.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ducode.Essentials.Mail
{
   /// <summary>
   /// A static class for registering all needed classes for working with mails.
   /// </summary>
   public static class DependencyRegistration
   {
      /// <summary>
      /// A static method for registering all needed classes for working with mails.
      /// </summary>
      /// <typeparam name="TSmtpSettingsProvider">The implementation type of the SMTP settings provider.</typeparam>
      /// <param name="services">The services.</param>
      /// <returns>The <see cref="IServiceCollection"/>.</returns>
      public static IServiceCollection AddMailServices<TSmtpSettingsProvider>(this IServiceCollection services) where TSmtpSettingsProvider : class, ISmtpSettingsProvider
      {
         services.TryAddTransient<IMailService, MailService>();
         services.TryAddTransient<ISmtpSettingsProvider, TSmtpSettingsProvider>();
         return services;
      }
   }
}
