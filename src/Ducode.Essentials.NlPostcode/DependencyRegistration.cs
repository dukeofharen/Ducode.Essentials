using Ducode.Essentials.NlPostcode.Interfaces;
using Ducode.Essentials.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ducode.Essentials.NlPostcode
{
   /// <summary>
   /// A static class for registering all needed classes for the NL postcode lookup.
   /// </summary>
   public static class DependencyRegistration
   {
      /// <summary>
      /// A method for registering all needed classes for the NL postcode lookup.
      /// </summary>
      /// <param name="services">The services.</param>
      /// <returns>The <see cref="IServiceCollection"/>.</returns>
      public static IServiceCollection AddNlPostcode(this IServiceCollection services)
      {
         services
            .AddWebServices()
            .TryAddSingleton<IPostcodeService, PostcodeService>();
         return services;
      }
   }
}
