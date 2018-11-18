using System;
using System.Globalization;

namespace Ducode.Essentials.Characters
{
   /// <summary>
   /// A static class containing several methods for parsing one type to another.
   /// </summary>
   public static class ParseUtils
   {
      /// <summary>
      /// Safely parses a string to an int. If the string could not be parsed, null is returned.
      /// </summary>
      /// <param name="input">The input.</param>
      /// <returns>An int, or null if the input could not be parsed.</returns>
      public static int? SafeParseInt(string input)
      {
         if (!int.TryParse(input, out int result))
         {
            return null;
         }

         return result;
      }

      /// <summary>
      /// Safely parses a string to a double. If the string could not be parsed, null is returned.
      /// </summary>
      /// <param name="input">The input.</param>
      /// <returns>A double, or null if the input could not be parsed.</returns>
      public static double? SafeParseDouble(string input)
      {
         return SafeParseDouble(input, NumberStyles.Any, CultureInfo.InvariantCulture);
      }

      /// <summary>
      /// Safely parses a string to a double. If the string could not be parsed, null is returned.
      /// </summary>
      /// <param name="input">The input.</param>
      /// <param name="style">The style.</param>
      /// <param name="provider">The provider.</param>
      /// <returns>
      /// A double, or null if the input could not be parsed.
      /// </returns>
      public static double? SafeParseDouble(string input, NumberStyles style, IFormatProvider provider)
      {
         if (!double.TryParse(input, style, provider, out double result))
         {
            return null;
         }

         return result;
      }

      /// <summary>
      /// Safely parses a string to a DateTime. If the string could not be parsed, null is returned.
      /// </summary>
      /// <param name="input">The input.</param>
      /// <param name="format">The format.</param>
      /// <param name="provider">The provider.</param>
      /// <returns>A DateTime, or null if the input could not be parsed.</returns>
      public static DateTime? SafeParseDateTime(string input, string format, IFormatProvider provider)
      {
         if (!DateTime.TryParseExact(input, format, provider, DateTimeStyles.None, out var result))
         {
            return null;
         }

         return result;
      }
   }
}
