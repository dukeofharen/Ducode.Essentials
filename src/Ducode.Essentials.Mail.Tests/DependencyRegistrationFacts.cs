using Ducode.Essentials.Mail.Interfaces;
using Ducode.Essentials.Mail.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ducode.Essentials.Mail.Tests
{
   [TestClass]
   public class DependencyRegistrationFacts
   {
      [TestMethod]
      public void DependencyRegistration_AddMailServices_HappyFlow()
      {
         // arrange
         var services = new ServiceCollection();

         // act
         services.AddMailServices<TestSmtpSettingsProvider>();
         var provider = services.BuildServiceProvider();
         var service = provider.GetService<IMailService>();

         // assert
         Assert.IsNotNull(service);
      }

      public class TestSmtpSettingsProvider : ISmtpSettingsProvider
      {
         public SmtpSettingsModel GetSmtpSettings()
         {
            throw new System.NotImplementedException();
         }
      }
   }
}
