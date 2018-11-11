using Ducode.Essentials.EntityFramework.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ducode.Essentials.EntityFramework
{
   /// <summary>
   /// A static class for registering all needed classes for working with EntityFramework.
   /// </summary>
   public static class DependencyRegistration
   {
      /// <summary>
      /// A static method for registering all needed classes for working with EntityFramework.
      /// </summary>
      /// <typeparam name="TUoWFactoryInterface">The type of the unit of work factory interface.</typeparam>
      /// <typeparam name="TUoWFactorInstance">The type of the unit of work factory instance.</typeparam>
      /// <param name="services">The services.</param>
      /// <returns>The <see cref="IServiceCollection"/>.</returns>
      public static IServiceCollection AddEntityFrameworkServices<TUoWFactoryInterface, TUoWFactorInstance>(this IServiceCollection services)
         where TUoWFactoryInterface : class, IUnitOfWorkFactory
         where TUoWFactorInstance : class, TUoWFactoryInterface
      {
         services.TryAddTransient<TUoWFactoryInterface, TUoWFactorInstance>();
         return services;
      }
   }
}
