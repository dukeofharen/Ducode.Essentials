using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ducode.Essentials.Mvc.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;

namespace Ducode.Essentials.Mvc
{
   /// <summary>
   /// A class that contains several HTTP request / response related methods.
   /// </summary>
   /// <seealso cref="Ducode.Essentials.Mvc.Interfaces.IHttpContextService" />
   public class HttpContextService : IHttpContextService
   {
      private const string ForwardedHostKey = "X-Forwarded-Host";
      private const string ForwardedProtoKey = "X-Forwarded-Proto";
      private readonly IHttpContextAccessor _httpContextAccessor;

      /// <summary>
      /// Initializes a new instance of the <see cref="HttpContextService"/> class.
      /// </summary>
      /// <param name="httpContextAccessor">The HTTP context accessor.</param>
      public HttpContextService(IHttpContextAccessor httpContextAccessor)
      {
         _httpContextAccessor = httpContextAccessor;
      }

      /// <summary>
      /// Gets the HTTP method.
      /// </summary>
      /// <value>
      /// The HTTP method.
      /// </value>
      public string Method => _httpContextAccessor.HttpContext.Request.Method;

      /// <summary>
      /// Gets the request path.
      /// </summary>
      /// <value>
      /// The request path.
      /// </value>
      public string Path => _httpContextAccessor.HttpContext.Request.Path;

      /// <summary>
      /// Gets the request path + query string.
      /// </summary>
      /// <value>
      /// The request path + query string.
      /// </value>
      public string FullPath => $"{_httpContextAccessor.HttpContext.Request.Path}{_httpContextAccessor.HttpContext.Request.QueryString}";

      /// <summary>
      /// Gets the display URL.
      /// </summary>
      /// <value>
      /// The display URL.
      /// </value>
      public string DisplayUrl => _httpContextAccessor.HttpContext.Request.GetDisplayUrl();

      /// <summary>
      /// Gets the posted body.
      /// </summary>
      /// <returns>
      /// The posted body.
      /// </returns>
      public string GetBody()
      {
         using (var bodyStream = new MemoryStream())
         using (var reader = new StreamReader(bodyStream))
         {
            _httpContextAccessor.HttpContext.Request.Body.CopyTo(bodyStream);
            _httpContextAccessor.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            bodyStream.Seek(0, SeekOrigin.Begin);
            var body = reader.ReadToEnd();
            return body;
         }
      }

      /// <summary>
      /// Gets the query strings as dictionary.
      /// </summary>
      /// <returns>
      /// The query strings as dictionary.
      /// </returns>
      public IDictionary<string, string> GetQueryStringDictionary()
      {
         return _httpContextAccessor.HttpContext.Request.Query
            .ToDictionary(q => q.Key, q => q.Value.ToString());
      }

      /// <summary>
      /// Gets the request headers.
      /// </summary>
      /// <returns>
      /// The request headers.
      /// </returns>
      public IDictionary<string, string> GetHeaders()
      {
         return _httpContextAccessor.HttpContext.Request.Headers
            .ToDictionary(h => h.Key, h => h.Value.ToString());
      }

      /// <summary>
      /// Gets an item from the HTTP context..
      /// </summary>
      /// <typeparam name="TObject">The type of the object.</typeparam>
      /// <param name="key">The key.</param>
      /// <returns>
      /// The item.
      /// </returns>
      public TObject GetItem<TObject>(string key)
      {
         var item = _httpContextAccessor.HttpContext?.Items[key];
         return (TObject)item;
      }

      /// <summary>
      /// Sets an item on the HTTP context.
      /// </summary>
      /// <param name="key">The key.</param>
      /// <param name="item">The item.</param>
      public void SetItem(string key, object item)
      {
         _httpContextAccessor.HttpContext?.Items.Add(key, item);
      }

      

      /// <summary>
      /// Gets the request host.
      /// </summary>
      /// <returns>
      /// The request host.
      /// </returns>
      public string GetHost()
      {
         var request = _httpContextAccessor.HttpContext.Request;
         var header = request.Headers.FirstOrDefault(h => h.Key?.Equals(ForwardedHostKey, StringComparison.OrdinalIgnoreCase) == true);
         if (header.Key != null)
         {
            // TODO in a later stage, check the reverse proxy against a list of "safe" proxy IPs.
            return header.Value;
         }
         else
         {
            return request.Host.ToString();
         }
      }

      /// <summary>
      /// Determines whether the request is made through HTTPS.
      /// </summary>
      /// <returns>
      /// <c>true</c> if the request is made through HTTPS; otherwise, <c>false</c>.
      /// </returns>
      public bool IsHttps()
      {
         var request = _httpContextAccessor.HttpContext.Request;
         var header = request.Headers.FirstOrDefault(h => h.Key?.Equals(ForwardedProtoKey, StringComparison.OrdinalIgnoreCase) == true);
         if (header.Key != null)
         {
            // TODO in a later stage, check the reverse proxy against a list of "safe" proxy IPs.
            return header.Value.ToString().Equals("https", StringComparison.OrdinalIgnoreCase);
         }
         else
         {
            return request.IsHttps;
         }
      }

      /// <summary>
      /// Gets the request form values.
      /// </summary>
      /// <returns>
      /// The request form values.
      /// </returns>
      public (string, StringValues)[] GetFormValues()
      {
         var httpContext = _httpContextAccessor.HttpContext;
         return httpContext.Request.Form
             .Select(f => (f.Key, f.Value))
             .ToArray();
      }

      /// <summary>
      /// Sets the response status code.
      /// </summary>
      /// <param name="statusCode">The response status code.</param>
      public void SetStatusCode(int statusCode)
      {
         var httpContext = _httpContextAccessor.HttpContext;
         httpContext.Response.StatusCode = statusCode;
      }

      /// <summary>
      /// Adds a response header.
      /// </summary>
      /// <param name="key">The key.</param>
      /// <param name="values">The values.</param>
      public void AddHeader(string key, StringValues values)
      {
         var httpContext = _httpContextAccessor.HttpContext;
         httpContext.Response.Headers.Add(key, values);
      }

      /// <summary>
      /// Tries to add a response header.
      /// </summary>
      /// <param name="key">The key.</param>
      /// <param name="values">The values.</param>
      /// <returns>
      /// True if added, false otherwise.
      /// </returns>
      public bool TryAddHeader(string key, StringValues values)
      {
         var httpContext = _httpContextAccessor.HttpContext;
         if (!httpContext.Response.Headers.ContainsKey(key))
         {
            httpContext.Response.Headers.Add(key, values);
            return true;
         }

         return false;
      }

      /// <summary>
      /// Enables rewind on the HTTP context.
      /// </summary>
      public void EnableRewind()
      {
         var httpContext = _httpContextAccessor.HttpContext;
         httpContext.Request.EnableRewind();
      }

      /// <summary>
      /// Clears the response.
      /// </summary>
      public void ClearResponse()
      {
         var httpContext = _httpContextAccessor.HttpContext;
         httpContext.Response.Clear();
      }

      /// <summary>
      /// Writes a byte array to the response asynchronous.
      /// </summary>
      /// <param name="body">The body.</param>
      /// <returns>
      /// A task.
      /// </returns>
      public async Task WriteAsync(byte[] body)
      {
         var httpContext = _httpContextAccessor.HttpContext;
         await httpContext.Response.Body.WriteAsync(body, 0, body.Length);
      }
   }
}
