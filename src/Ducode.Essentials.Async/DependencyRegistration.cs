using Ducode.Essentials.Async.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ducode.Essentials.Async
{
   /// <summary>
   ///  A static class for registering all needed classes for working with threads and tasks.
   /// </summary>
   public static class DependencyRegistration
   {
      /// <summary>
      /// A static method for registering all needed classes for working threads and tasks.
      /// </summary>
      /// <param name="services">The services.</param>
      /// <returns></returns>
      public static IServiceCollection AddAsyncServices(this IServiceCollection services)
      {
         services.TryAddSingleton<IAsyncService, AsyncService>();
         return services;
      }
   }
}
