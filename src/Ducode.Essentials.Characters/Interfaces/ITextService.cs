namespace Ducode.Essentials.Characters.Interfaces
{
   /// <summary>
   /// Describes a class that contains several string related functions.
   /// </summary>
   public interface ITextService
   {
      /// <summary>
      /// Generates a numeric string with a given length.
      /// </summary>
      /// <param name="length">The length.</param>
      /// <returns>A numeric string.</returns>
      string GenerateNumericString(int length = 6);

      /// <summary>
      /// Strips all non alphanumeric characters from a given string.
      /// </summary>
      /// <param name="input">The input.</param>
      /// <returns>The stripped string.</returns>
      string StripNonAlphanumericCharacters(string input);

      /// <summary>
      /// Generates a random password based on letters.
      /// </summary>
      /// <param name="length">The password length.</param>
      /// <returns>A random password.</returns>
      string GenerateRandomPassword(int length = 6);

      /// <summary>
      /// Strips HTML characters from the string.
      /// </summary>
      /// <param name="input">The input.</param>
      /// <returns>The stripped string.</returns>
      string StripHtml(string input);

      /// <summary>
      /// Determines whether the input string is a valid email address.
      /// If the input are multiple addresses separated by ",", the separated email addresses will be validated.
      /// </summary>
      /// <param name="email">The email.</param>
      /// <returns>
      ///   <c>true</c> if the input is valid; otherwise, <c>false</c>.
      /// </returns>
      bool IsValidEmail(string email);
   }
}
