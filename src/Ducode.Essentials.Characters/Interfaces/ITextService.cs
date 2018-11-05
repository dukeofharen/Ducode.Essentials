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
   }
}
