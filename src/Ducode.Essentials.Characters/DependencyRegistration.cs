using Ducode.Essentials.Characters.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ducode.Essentials.Characters
{
   /// <summary>
   /// A static class for registering all needed classes for working with characters.
   /// </summary>
   public static class DependencyRegistration
   {
      /// <summary>
      /// A static method for registering all needed classes for working with characters.
      /// </summary>
      /// <param name="services">The services.</param>
      /// <returns></returns>
      public static IServiceCollection AddCharactersServices(this IServiceCollection services)
      {
         services.TryAddSingleton<ITextService, TextService>();
         return services;
      }
   }
}
