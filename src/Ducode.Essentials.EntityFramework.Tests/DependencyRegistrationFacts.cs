using System;
using Ducode.Essentials.EntityFramework.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ducode.Essentials.EntityFramework.Tests
{
   [TestClass]
   public class DependencyRegistrationFacts
   {
      [TestMethod]
      public void DependencyRegistration_AddEntityFrameworkServices_HappyFlow()
      {
         // arrange
         var services = new ServiceCollection();

         // act
         services.AddEntityFrameworkServices<ICustomUnitOfWorkFactory, CustomUnitOfWorkFactory>();
         var provider = services.BuildServiceProvider();
         var factory = provider.GetService<ICustomUnitOfWorkFactory>();

         // assert
         Assert.IsNotNull(factory);
      }

      public interface ICustomUnitOfWorkFactory : IUnitOfWorkFactory
      {
      }

      public class CustomUnitOfWorkFactory : ICustomUnitOfWorkFactory
      {
         public IUnitOfWork Create()
         {
            throw new NotImplementedException();
         }
      }
   }
}
