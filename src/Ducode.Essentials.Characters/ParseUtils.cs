using System;
using System.Globalization;

namespace Ducode.Essentials.Characters
{
   public static class ParseUtils
   {
      public static int? SafeParseInt(string input)
      {
         if (!int.TryParse(input, out int result))
         {
            return null;
         }

         return result;
      }

      public static double? SafeParseDouble(string input)
      {
         return SafeParseDouble(input, NumberStyles.Any, CultureInfo.InvariantCulture);
      }

      public static double? SafeParseDouble(string input, NumberStyles style, IFormatProvider provider)
      {
         if (!double.TryParse(input, style, provider, out double result))
         {
            return null;
         }

         return result;
      }

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
