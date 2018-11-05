using Ducode.Essentials.Mvc.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ducode.Essentials.Mvc
{
   /// <summary>
   /// A static class for registering all needed classes for extra MVC functionality.
   /// </summary>
   public static class DependencyRegistration
   {
      /// <summary>
      /// A static method for registering all needed classes for extra MVC functionality.
      /// </summary>
      /// <param name="services">The services.</param>
      /// <returns>The <see cref="IServiceCollection"/>.</returns>
      public static IServiceCollection AddCustomMvcServices(this IServiceCollection services)
      {
         services.TryAddSingleton<IClientIpResolver, ClientIpResolver>();
         services.TryAddSingleton<IHttpContextService, HttpContextService>();
         return services;
      }
   }
}
