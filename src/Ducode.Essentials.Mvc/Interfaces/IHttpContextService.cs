using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace Ducode.Essentials.Mvc.Interfaces
{
   /// <summary>
   /// Describes a class that contains several HTTP request / response related methods.
   /// </summary>
   public interface IHttpContextService
   {
      /// <summary>
      /// Gets the HTTP method.
      /// </summary>
      /// <value>
      /// The HTTP method.
      /// </value>
      string Method { get; }

      /// <summary>
      /// Gets the request path.
      /// </summary>
      /// <value>
      /// The request path.
      /// </value>
      string Path { get; }

      /// <summary>
      /// Gets the request path + query string.
      /// </summary>
      /// <value>
      /// The request path + query string.
      /// </value>
      string FullPath { get; }

      /// <summary>
      /// Gets the display URL.
      /// </summary>
      /// <value>
      /// The display URL.
      /// </value>
      string DisplayUrl { get; }

      /// <summary>
      /// Gets the posted body.
      /// </summary>
      /// <returns>The posted body.</returns>
      string GetBody();

      /// <summary>
      /// Gets the query strings as dictionary.
      /// </summary>
      /// <returns>The query strings as dictionary.</returns>
      IDictionary<string, string> GetQueryStringDictionary();

      /// <summary>
      /// Gets the request headers.
      /// </summary>
      /// <returns>The request headers.</returns>
      IDictionary<string, string> GetHeaders();

      /// <summary>
      /// Gets an item from the HTTP context..
      /// </summary>
      /// <typeparam name="TObject">The type of the object.</typeparam>
      /// <param name="key">The key.</param>
      /// <returns>The item.</returns>
      TObject GetItem<TObject>(string key);

      /// <summary>
      /// Sets an item on the HTTP context.
      /// </summary>
      /// <param name="key">The key.</param>
      /// <param name="item">The item.</param>
      void SetItem(string key, object item);

      /// <summary>
      /// Gets the request host.
      /// </summary>
      /// <returns>The request host.</returns>
      string GetHost();

      /// <summary>
      /// Determines whether the request is made through HTTPS.
      /// </summary>
      /// <returns>
      ///   <c>true</c> if the request is made through HTTPS; otherwise, <c>false</c>.
      /// </returns>
      bool IsHttps();

      /// <summary>
      /// Gets the request form values.
      /// </summary>
      /// <returns>The request form values.</returns>
      (string, StringValues)[] GetFormValues();

      /// <summary>
      /// Sets the response status code.
      /// </summary>
      /// <param name="statusCode">The response status code.</param>
      void SetStatusCode(int statusCode);

      /// <summary>
      /// Adds a response header.
      /// </summary>
      /// <param name="key">The key.</param>
      /// <param name="values">The values.</param>
      void AddHeader(string key, StringValues values);

      /// <summary>
      /// Tries to add a response header.
      /// </summary>
      /// <param name="key">The key.</param>
      /// <param name="values">The values.</param>
      /// <returns>True if added, false otherwise.</returns>
      bool TryAddHeader(string key, StringValues values);

      /// <summary>
      /// Enables rewind on the HTTP context.
      /// </summary>
      void EnableRewind();

      /// <summary>
      /// Clears the response.
      /// </summary>
      void ClearResponse();

      /// <summary>
      /// Writes a byte array to the response asynchronous.
      /// </summary>
      /// <param name="body">The body.</param>
      /// <returns>A task.</returns>
      Task WriteAsync(byte[] body);
   }
}
