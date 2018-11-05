using Ducode.Essentials.Dates.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ducode.Essentials.Dates
{
   /// <summary>
   /// A static class for registering all needed classes for working with dates and times.
   /// </summary>
   public static class DependencyRegistration
   {
      /// <summary>
      /// A method for registering all needed dependencies for working with dates and times.
      /// </summary>
      /// <param name="services">The services.</param>
      /// <returns>The <see cref="IServiceCollection"/>.</returns>
      public static IServiceCollection AddDatesServices(this IServiceCollection services)
      {
         services.TryAddSingleton<IDateTimeService, DateTimeService>();
         return services;
      }
   }
}
