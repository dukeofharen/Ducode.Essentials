using System;

namespace Ducode.Essentials.CommonUtils
{
   /// <summary>
   /// A static class containing several methods for working with objects.
   /// </summary>
   public static class ObjectUtilities
   {
      /// <summary>
      /// A shorthand method for fluently setting values on an object.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="obj">The object.</param>
      /// <param name="action">The action.</param>
      /// <returns>The same object used as input.</returns>
      public static T Set<T>(this T obj, Action<T> action)
      {
         action(obj);
         return obj;
      }
   }
}
