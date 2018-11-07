using Ducode.Essentials.Mvc.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Ducode.Essentials.Mvc
{
   /// <summary>
   /// A class that is a convenient wrapper around the MVC session.
   /// </summary>
   /// <seealso cref="Ducode.Essentials.Mvc.Interfaces.ISessionService" />
   public class SessionService : ISessionService
   {
      private readonly IHttpContextAccessor _httpContextAccessor;

      /// <summary>
      /// Initializes a new instance of the <see cref="SessionService"/> class.
      /// </summary>
      /// <param name="httpContextAccessor">The HTTP context accessor.</param>
      public SessionService(IHttpContextAccessor httpContextAccessor)
      {
         _httpContextAccessor = httpContextAccessor;
      }

      /// <summary>
      /// Gets a value from the session.
      /// </summary>
      /// <typeparam name="TValue">The type of the value.</typeparam>
      /// <param name="key">The key.</param>
      /// <returns>
      /// The value from the session.
      /// </returns>
      public TValue GetValue<TValue>(string key)
      {
         string value = _httpContextAccessor.HttpContext.Session.GetString(key);
         return string.IsNullOrWhiteSpace(value) ? default(TValue) : JsonConvert.DeserializeObject<TValue>(value);

      }

      /// <summary>
      /// Sets a value on the session.
      /// </summary>
      /// <typeparam name="TValue">The type of the value.</typeparam>
      /// <param name="key">The key.</param>
      /// <param name="value">The value.</param>
      public void SetValue<TValue>(string key, TValue value)
      {
         string serializedValue = JsonConvert.SerializeObject(value);
         _httpContextAccessor.HttpContext.Session.SetString(key, serializedValue);
      }

      /// <summary>
      /// Removes a value from the session.
      /// </summary>
      /// <param name="key">The key.</param>
      public void RemoveValue(string key)
      {
         _httpContextAccessor.HttpContext.Session.Remove(key);
      }
   }
}
