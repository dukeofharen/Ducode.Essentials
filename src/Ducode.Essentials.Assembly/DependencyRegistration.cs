using Ducode.Essentials.Assembly.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ducode.Essentials.Assembly
{
   /// <summary>
   /// A static class for registering all needed classes for working with assemblies.
   /// </summary>
   public static class DependencyRegistration
   {
      /// <summary>
      /// A method for registering all dependencies for registering all needed classes for working with assemblies.
      /// </summary>
      /// <param name="services">The services.</param>
      /// <returns>The <see cref="IServiceCollection"/>.</returns>
      public static IServiceCollection AddAssemblyServices(this IServiceCollection services)
      {
         services.TryAddSingleton<IAssemblyService, AssemblyService>();
         return services;
      }
   }
}
