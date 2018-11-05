using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ducode.Essentials.Characters.Tests
{
   [TestClass]
   public class TextHelperFacts
   {
      [TestMethod]
      public void TextHelper_GenerateNumericString_HappyFlow()
      {
         // arrange
         int howMany = 20;

         // act
         string result = TextHelper.GenerateNumericString(howMany);

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(result));
         Assert.AreEqual(howMany, result.Length);
      }

      [TestMethod]
      public void TextHelper_StripNonAlphanumericCharacters_HappyFlow()
      {
         // arrange
         string input = "Thïs ïs - â test &";
         string expectedOutput = "Ths s -  test ";

         // act
         string result = TextHelper.StripNonAlphanumericCharacters(input);

         // assert
         Assert.AreEqual(expectedOutput, result);
      }
   }
}