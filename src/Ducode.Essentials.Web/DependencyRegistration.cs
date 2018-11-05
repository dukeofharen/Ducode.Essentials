using Ducode.Essentials.Web.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ducode.Essentials.Web
{
   /// <summary>
   /// A class for registering web related dependencies.
   /// </summary>
   public static class DependencyRegistration
   {
      /// <summary>
      /// Registers all web related dependencies.
      /// </summary>
      /// <param name="services">The services.</param>
      /// <returns>The <see cref="IServiceCollection"/>.</returns>
      public static IServiceCollection AddWebServices(this IServiceCollection services)
      {
         services.TryAddSingleton<IWebService, WebService>();
         return services;
      }
   }
}
