using Ducode.Essentials.Characters.Interfaces;

namespace Ducode.Essentials.Characters
{
   /// <summary>
   /// A class that contains several string related functions.
   /// </summary>
   /// <seealso cref="ITextService" />
   public class TextService : ITextService
   {
      /// <summary>
      /// Generates a numeric string with a given length.
      /// </summary>
      /// <param name="length">The length.</param>
      /// <returns>
      /// A numeric string.
      /// </returns>
      public string GenerateNumericString(int length = 6)
      {
         return TextHelper.GenerateNumericString(length);
      }

      /// <summary>
      /// Generates a random password based on letters.
      /// </summary>
      /// <param name="length">The password length.</param>
      /// <returns>
      /// A random password.
      /// </returns>
      public string GenerateRandomPassword(int length = 6)
      {
         return TextHelper.GenerateRandomPassword(length);
      }

      /// <summary>
      /// Determines whether the input string is a valid email address.
      /// If the input are multiple addresses separated by ",", the separated email addresses will be validated.
      /// </summary>
      /// <param name="email">The email.</param>
      /// <returns>
      /// <c>true</c> if the input is valid; otherwise, <c>false</c>.
      /// </returns>
      public bool IsValidEmail(string email)
      {
         return email.IsValidEmail();
      }

      /// <summary>
      /// Strips HTML characters from the string.
      /// </summary>
      /// <param name="input">The input.</param>
      /// <returns>
      /// The stripped string.
      /// </returns>
      public string StripHtml(string input)
      {
         return input.StripHtml();
      }

      /// <summary>
      /// Strips all non alphanumeric characters from a given string.
      /// </summary>
      /// <param name="input">The input.</param>
      /// <returns>
      /// The stripped string.
      /// </returns>
      public string StripNonAlphanumericCharacters(string input)
      {
         return TextHelper.StripNonAlphanumericCharacters(input);
      }
   }
}
