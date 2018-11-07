using System;
using System.Linq;
using Ducode.Essentials.Mvc.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Ducode.Essentials.Mvc
{
   /// <summary>
   /// A class used for retrieving the IP address from a client.
   /// </summary>
   public class ClientIpResolver : IClientIpResolver
   {
      private const string ForwardedHeaderKey = "X-Forwarded-For";
      private readonly IHttpContextAccessor _httpContextAccessor;

      /// <summary>
      /// Initializes a new instance of the <see cref="ClientIpResolver"/> class.
      /// </summary>
      /// <param name="httpContextAccessor">The HTTP context accessor.</param>
      public ClientIpResolver(IHttpContextAccessor httpContextAccessor)
      {
         _httpContextAccessor = httpContextAccessor;
      }

      /// <summary>
      /// Gets the client IP address.
      /// </summary>
      /// <returns>
      /// The client IP address.
      /// </returns>
      public string GetClientIp()
      {
         var request = _httpContextAccessor.HttpContext.Request;
         string actualIp = IpUtilities.NormalizeIp(_httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString());
         var forwardedHeader = request.Headers.FirstOrDefault(h => h.Key?.Equals(ForwardedHeaderKey, StringComparison.OrdinalIgnoreCase) == true);
         if (forwardedHeader.Key != null && actualIp == "127.0.0.1")
         {
            // TODO in a later stage, check the reverse proxy against a list of "safe" proxy IPs.
            string forwardedFor = forwardedHeader.Value;
            var parts = forwardedFor.Split(new[] { ", " }, StringSplitOptions.None);
            string forwardedIp = IpUtilities.NormalizeIp(parts.First());
            return forwardedIp;
         }

         return actualIp;
      }
   }
}
