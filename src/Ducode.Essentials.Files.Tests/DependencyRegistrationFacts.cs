using Ducode.Essentials.Files;
using Ducode.Essentials.Files.Interfaces;
using Ducode.Essentials.Files.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ducode.Essentials.Mail.Tests
{
   [TestClass]
   public class DependencyRegistrationFacts
   {
      [TestMethod]
      public void DependencyRegistration_AddFileCachingServices_HappyFlow()
      {
         // arrange
         var services = new ServiceCollection();

         // act
         services.AddFileCachingServices<TestFileCachingSettingsProvider>();
         var provider = services.BuildServiceProvider();
         var service = provider.GetService<IFileCachingService>();

         // assert
         Assert.IsNotNull(service);
      }

      public class TestFileCachingSettingsProvider : IFileCachingSettingsProvider
      {
         public FileCachingSettingsModel GetFileCachingSettings()
         {
            throw new System.NotImplementedException();
         }
      }
   }
}
