using Ducode.Essentials.Characters.Interfaces;

namespace Ducode.Essentials.Characters
{
   /// <summary>
   /// A class that contains several string related functions.
   /// </summary>
   /// <seealso cref="Ducode.Essentials.Characters.Interfaces.ITextService" />
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
