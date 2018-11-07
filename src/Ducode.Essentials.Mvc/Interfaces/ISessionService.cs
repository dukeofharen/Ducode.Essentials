namespace Ducode.Essentials.Mvc.Interfaces
{
   /// <summary>
   /// Describes a class that is a convenient wrapper around the MVC session.
   /// </summary>
   public interface ISessionService
   {
      /// <summary>
      /// Gets a value from the session.
      /// </summary>
      /// <typeparam name="TValue">The type of the value.</typeparam>
      /// <param name="key">The key.</param>
      /// <returns>The value from the session.</returns>
      TValue GetValue<TValue>(string key);

      /// <summary>
      /// Sets a value on the session.
      /// </summary>
      /// <typeparam name="TValue">The type of the value.</typeparam>
      /// <param name="key">The key.</param>
      /// <param name="value">The value.</param>
      void SetValue<TValue>(string key, TValue value);

      /// <summary>
      /// Removes a value from the session.
      /// </summary>
      /// <param name="key">The key.</param>
      void RemoveValue(string key);
   }
}
