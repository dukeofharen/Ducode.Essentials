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

      [TestMethod]
      public void TextHelper_GenerateRandomPassword_HappyFlow()
      {
         // arrange
         int length = 10;

         // act
         string password = TextHelper.GenerateRandomPassword(length);

         // assert
         Assert.AreEqual(10, password.Length);
      }

      [TestMethod]
      public void TextHelper_StripHtml_HappyFlow()
      {
         // arrange
         string input = "<strong><em>Test</em></strong><p>String</p>";
         string expectedOutput = "TestString";

         // act
         string result = TextHelper.StripHtml(input);

         // assert
         Assert.AreEqual(expectedOutput, result);
      }

      [TestMethod]
      public void TextHelper_IsValidEmail_Single_Invalid_ShouldReturnFalse()
      {
         // arrange
         string input = "bla invalid";

         // act
         bool result = input.IsValidEmail();

         // assert
         Assert.IsFalse(result);
      }

      [TestMethod]
      public void TextHelper_IsValidEmail_Single_Valid_ShouldReturnTrue()
      {
         // arrange
         string input = "test@gmail.com";

         // act
         bool result = input.IsValidEmail();

         // assert
         Assert.IsTrue(result);
      }

      [TestMethod]
      public void TextHelper_IsValidEmail_Multiple_Invalid_ShouldReturnFalse()
      {
         // arrange
         string input = "test@gmail.com,bla invalid";

         // act
         bool result = input.IsValidEmail();

         // assert
         Assert.IsFalse(result);
      }

      [TestMethod]
      public void TextHelper_IsValidEmail_Multiple_Valid_ShouldReturnTrue()
      {
         // arrange
         string input = "test@gmail.com,test1@gmail.com";

         // act
         bool result = input.IsValidEmail();

         // assert
         Assert.IsTrue(result);
      }
   }
}