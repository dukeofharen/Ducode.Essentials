using Ducode.Essentials.Validations.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ducode.Essentials.Validations
{
   /// <summary>
   /// A static class for registering all needed classes for working with validations
   /// </summary>
   public static class DependencyRegistration
   {
      /// <summary>
      /// A static method for registering all needed classes for working with validations.
      /// </summary>
      /// <param name="services">The services.</param>
      /// <returns>The <see cref="IServiceCollection"/>.</returns>
      public static IServiceCollection AddValidationsServices(this IServiceCollection services)
      {
         services.TryAddSingleton<IModelValidator, ModelValidator>();
         return services;
      }
   }
}
