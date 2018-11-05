using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Ducode.Essentials.Web.Interfaces;

namespace Ducode.Essentials.Web
{
   /// <summary>
   /// A class that is used to do HTTP calls.
   /// </summary>
   public class WebService : IWebService
   {
      // Static HTTP client: https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
      // TODO check if this is still needed for .NET Core.
      private static readonly HttpClient HttpClient = new HttpClient();

      /// <summary>
      /// Performs a GET request asynchronous.
      /// </summary>
      /// <param name="url">The URL.</param>
      /// <returns>
      /// A <see cref="T:System.Net.Http.HttpResponseMessage" />
      /// </returns>
      public async Task<HttpResponseMessage> GetAsync(string url)
      {
         return await GetAsync(url, new Dictionary<string, string>());
      }

      /// <summary>
      /// Performs a GET request with headers asynchronous.
      /// </summary>
      /// <param name="url">The URL.</param>
      /// <param name="headers">The headers.</param>
      /// <returns>
      /// A <see cref="T:System.Net.Http.HttpResponseMessage" />
      /// </returns>
      public async Task<HttpResponseMessage> GetAsync(string url, IDictionary<string, string> headers)
      {
         var message = new HttpRequestMessage
         {
            RequestUri = new Uri(url)
         };
         AddHeaders(message, headers);
         var response = await SendAsync(message);
         return response;
      }

      /// <summary>
      /// Sends an HTTP request asynchronous
      /// </summary>
      /// <param name="message">The message.</param>
      /// <returns>
      /// A <see cref="T:System.Net.Http.HttpResponseMessage" />
      /// </returns>
      public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage message)
      {
         var response = await HttpClient.SendAsync(message);
         return response;
      }

      private static void AddHeaders(HttpRequestMessage message, IDictionary<string, string> headers)
      {
         foreach (var header in headers)
         {
            message.Headers.Add(header.Key, header.Value);
         }
      }
   }
}
