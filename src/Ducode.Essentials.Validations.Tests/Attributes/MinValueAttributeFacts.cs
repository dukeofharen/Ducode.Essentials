using Ducode.Essentials.Validations.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ducode.Essentials.Validations.Tests.Attributes
{
   [TestClass]
   public class MaValueAttributeFacts
   {
      [TestMethod]
      public void MinValueAttribute_IsValid_ValueIsHigherThanMin_ShouldReturnTrue()
      {
         // arrange
         int minValue = 5;
         int value = 6;
         var attribute = new MinValueAttribute(minValue);

         // act
         bool valid = attribute.IsValid(value);

         // assert
         Assert.IsTrue(valid);
      }

      [TestMethod]
      public void MinValueAttribute_IsValid_ValueIsEqualToMax_ShouldReturnTrue()
      {
         // arrange
         int minValue = 5;
         int value = 5;
         var attribute = new MinValueAttribute(minValue);

         // act
         bool valid = attribute.IsValid(value);

         // assert
         Assert.IsTrue(valid);
      }

      [TestMethod]
      public void MinValueAttribute_IsValid_ValueIsSmallerThanMax_ShouldReturnFalse()
      {
         // arrange
         int minValue = 5;
         int value = 4;
         var attribute = new MinValueAttribute(minValue);

         // act
         bool valid = attribute.IsValid(value);

         // assert
         Assert.IsFalse(valid);
      }
   }
}
