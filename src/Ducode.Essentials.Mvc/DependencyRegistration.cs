using Ducode.Essentials.Mvc.Interfaces;
using Ducode.Essentials.Mvc.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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
         services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
         services.TryAddSingleton<IClientIpResolver, ClientIpResolver>();
         services.TryAddSingleton<IHttpContextService, HttpContextService>();
         return services;
      }

      /// <summary>
      /// A static method for registering all needed classes for IP whitelisting functionality.
      /// </summary>
      /// <typeparam name="TIpWhitelistingSettingsProvider">The type of the ip whitelisting settings provider.</typeparam>
      /// <param name="services">The services.</param>
      /// <returns>The <see cref="IServiceCollection"/>.</returns>
      public static IServiceCollection AddIpWhitelisting<TIpWhitelistingSettingsProvider>(this IServiceCollection services)
         where TIpWhitelistingSettingsProvider : class, IIpWhitelistingSettingsProvider
      {
         services.AddCustomMvcServices();
         services.TryAddTransient<IIpWhitelistingSettingsProvider, TIpWhitelistingSettingsProvider>();
         return services;
      }

      /// <summary>
      /// A static method for adding the needed middleware for IP whitelisting.
      /// </summary>
      /// <param name="app">The application.</param>
      /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
      public static IApplicationBuilder UseIpWhitelisting(this IApplicationBuilder app)
      {
         app.UseMiddleware<IpWhitelistingMiddleware>();
         return app;
      }
   }
}
