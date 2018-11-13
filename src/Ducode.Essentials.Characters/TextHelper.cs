using System;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Ducode.Essentials.Characters
{
   /// <summary>
   /// A static class containing several text methods.
   /// </summary>
   public static class TextHelper
   {
      private static readonly Random _random = new Random();

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
            builder.Append(_random.Next(0, 10));
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

      /// <summary>
      /// Generates a random password based on letters.
      /// </summary>
      /// <param name="length">The password length.</param>
      /// <returns>A random password.</returns>
      public static string GenerateRandomPassword(int length = 6)
      {
         var builder = new StringBuilder();
         var letters = new[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
         for (int i = 0; i < length; i++)
         {
            builder.Append(letters[_random.Next(0, letters.Length - 1)]);
         }

         return builder.ToString();
      }

      /// <summary>
      /// Strips HTML characters from the string.
      /// </summary>
      /// <param name="input">The input.</param>
      /// <returns>The stripped string.</returns>
      public static string StripHtml(this string input)
      {
         if (input == null)
         {
            return string.Empty;
         }

         return Regex.Replace(input, "<.*?>", string.Empty);
      }

      /// <summary>
      /// Determines whether the input string is a valid email address.
      /// If the input are multiple addresses separated by ",", the separated email addresses will be validated.
      /// </summary>
      /// <param name="email">The email.</param>
      /// <returns>
      ///   <c>true</c> if the input is valid; otherwise, <c>false</c>.
      /// </returns>
      public static bool IsValidEmail(this string email)
      {
         var addresses = email.Split(',');
         foreach (string address in addresses)
         {
            try
            {
               var addr = new MailAddress(address);
            }
            catch
            {
               return false;
            }
         }

         return true;
      }
   }
}
