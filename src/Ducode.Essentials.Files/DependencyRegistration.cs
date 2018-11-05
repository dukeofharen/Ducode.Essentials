using Ducode.Essentials.Files.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ducode.Essentials.Files
{
   /// <summary>
   /// A static class for registering all needed classes for working with files.
   /// </summary>
   public static class DependencyRegistration
   {
      /// <summary>
      /// A method for registering all needed dependencies for working with files.
      /// </summary>
      /// <param name="services">The services.</param>
      /// <returns>The <see cref="IServiceCollection"/>.</returns>
      public static IServiceCollection AddFileServices(this IServiceCollection services)
      {
         services.TryAddSingleton<IFileService, FileService>();
         return services;
      }
   }
}
