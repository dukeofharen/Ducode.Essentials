using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Ducode.Essentials.Characters
{
   /// <summary>
   /// A static class containing several text methods.
   /// </summary>
   public static class TextHelper
   {
      private static readonly Random Random = new Random();

      /// <summary>
      /// Generates a numeric string with a given length.
      /// </summary>
      /// <param name="length">The length.</param>
      /// <returns>A numeric string.</returns>
      public static string GenerateNumericString(int length = 6)
      {
         var builder = new StringBuilder();
         for (int i = 0; i < length; i++)
         {
            builder.Append(Random.Next(0, 10));
         }

         return builder.ToString();
      }

      /// <summary>
      /// Strips all non alphanumeric characters from a given string.
      /// </summary>
      /// <param name="input">The input.</param>
      /// <returns>The stripped string.</returns>
      public static string StripNonAlphanumericCharacters(string input)
      {
         var regex = new Regex("[^a-zA-Z0-9 -]");
         return regex.Replace(input, string.Empty);
      }
   }
}
