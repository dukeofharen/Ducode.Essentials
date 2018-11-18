using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ducode.Essentials.Characters.Tests
{
   [TestClass]
   public class ParseUtilsFacts
   {
      [TestMethod]
      public void ParseUtils_SafeParseInt_InputInvalid_ShouldReturnNull()
      {
         // arrange
         string input = "invalid";

         // act
         var result = ParseUtils.SafeParseInt(input);

         // assert
         Assert.IsNull(result);
      }

      [TestMethod]
      public void ParseUtils_SafeParseInt_HappyFlow()
      {
         // arrange
         string input = "123";

         // act
         var result = ParseUtils.SafeParseInt(input);

         // assert
         Assert.AreEqual(123, result);
      }

      [TestMethod]
      public void ParseUtils_SafeParseDouble_InputInvalid_ShouldReturnNull()
      {
         // arrange
         string input = "invalid";

         // act
         var result = ParseUtils.SafeParseDouble(input);

         // assert
         Assert.IsNull(result);
      }

      [TestMethod]
      public void ParseUtils_SafeParseDouble_HappyFlow()
      {
         // arrange
         string input = "123.45";

         // act
         var result = ParseUtils.SafeParseDouble(input);

         // assert
         Assert.AreEqual(123.45, result);
      }

      [TestMethod]
      public void ParseUtils_SafeParseDouble_WithNumberStylesAndFormatProvider_InputInvalid_ShouldReturnNull()
      {
         // arrange
         string input = "invalid";

         // act
         var result = ParseUtils.SafeParseDouble(input, NumberStyles.Any, CultureInfo.InvariantCulture);

         // assert
         Assert.IsNull(result);
      }

      [TestMethod]
      public void ParseUtils_SafeParseDouble_WithNumberStylesAndFormatProvider_HappyFlow()
      {
         // arrange
         string input = "123.45";

         // act
         var result = ParseUtils.SafeParseDouble(input, NumberStyles.Any, CultureInfo.InvariantCulture);

         // assert
         Assert.AreEqual(123.45, result);
      }

      [TestMethod]
      public void ParseUtils_SafeParseDateTime_InputInvalid_ShouldReturnNull()
      {
         // arrange
         string input = "invalid";

         // act
         var result = ParseUtils.SafeParseDateTime(input, "dd-MM-yyyy", CultureInfo.InvariantCulture);

         // assert
         Assert.IsNull(result);
      }

      [TestMethod]
      public void ParseUtils_SafeParseDateTime_HappyFlow()
      {
         // arrange
         string input = "29-08-2018";
         var expectedResult = new DateTime(2018, 8, 29);

         // act
         var result = ParseUtils.SafeParseDateTime(input, "dd-MM-yyyy", CultureInfo.InvariantCulture);

         // assert
         Assert.AreEqual(expectedResult, result);
      }
   }
}
