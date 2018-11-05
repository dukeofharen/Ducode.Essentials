using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ducode.Essentials.Web.Interfaces
{
   /// <summary>
   /// Describes a class that is used to do HTTP calls.
   /// </summary>
   public interface IWebService
   {
      /// <summary>
      /// Performs a GET request asynchronous.
      /// </summary>
      /// <param name="url">The URL.</param>
      /// <returns>A <see cref="HttpResponseMessage"/></returns>
      Task<HttpResponseMessage> GetAsync(string url);

      /// <summary>
      /// Performs a GET request with headers asynchronous.
      /// </summary>
      /// <param name="url">The URL.</param>
      /// <param name="headers">The headers.</param>
      /// <returns>
      /// A <see cref="HttpResponseMessage" />
      /// </returns>
      Task<HttpResponseMessage> GetAsync(string url, IDictionary<string, string> headers);

      /// <summary>
      /// Sends an HTTP request asynchronous
      /// </summary>
      /// <param name="message">The message.</param>
      /// <returns>A <see cref="HttpResponseMessage"/></returns>
      Task<HttpResponseMessage> SendAsync(HttpRequestMessage message);
   }
}
