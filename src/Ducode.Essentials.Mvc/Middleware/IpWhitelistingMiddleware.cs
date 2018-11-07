using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ducode.Essentials.Mvc.Interfaces;
using Microsoft.AspNetCore.Http;
using NetTools;

namespace Ducode.Essentials.Mvc.Middleware
{
   /// <summary>
   /// A middleware useful for securing endpoints with IP whitelisting.
   /// </summary>
   public class IpWhitelistingMiddleware
   {
      private readonly IClientIpResolver _clientIpResolver;
      private readonly IHttpContextService _httpContextService;
      private readonly IIpWhitelistingSettingsProvider _ipWhitelistingSettingsProvider;
      private readonly RequestDelegate _next;

      /// <summary>
      /// Initializes a new instance of the <see cref="IpWhitelistingMiddleware"/> class.
      /// </summary>
      /// <param name="clientIpResolver">The client ip resolver.</param>
      /// <param name="httpContextService">The HTTP context service.</param>
      /// <param name="ipWhitelistingSettingsProvider">The ip whitelisting settings provider.</param>
      /// <param name="next">The next.</param>
      public IpWhitelistingMiddleware(
         IClientIpResolver clientIpResolver,
         IHttpContextService httpContextService,
         IIpWhitelistingSettingsProvider ipWhitelistingSettingsProvider,
         RequestDelegate next)
      {
         _clientIpResolver = clientIpResolver;
         _httpContextService = httpContextService;
         _ipWhitelistingSettingsProvider = ipWhitelistingSettingsProvider;
         _next = next;
      }

      /// <summary>
      /// Invokes the middleware.
      /// </summary>
      /// <param name="context">The context.</param>
      /// <returns>A task.</returns>
      public async Task Invoke(HttpContext context)
      {
         var whitelist = _ipWhitelistingSettingsProvider.GetIpWhitelistings();
         string path = _httpContextService.Path;
         var listing = whitelist?.FirstOrDefault(wl => path.IndexOf(wl.Path, StringComparison.OrdinalIgnoreCase) >= 0);
         if (listing != null)
         {
            // Check whether the current IP is allowed to access this path.
            var ipText = _clientIpResolver.GetClientIp();
            var ip = IPAddress.Parse(ipText);
            var ranges = listing.AllowedIps
               .Select(IPAddressRange.Parse);
            bool valid = ranges.Any(range => range.Contains(ip));
            if (!valid)
            {
               context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
            {
               await _next(context);
            }
         }
         else
         {
            await _next(context);
         }
      }
   }
}
