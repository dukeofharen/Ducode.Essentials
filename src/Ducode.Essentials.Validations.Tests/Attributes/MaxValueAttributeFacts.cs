using Ducode.Essentials.Validations.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ducode.Essentials.Validations.Tests.Attributes
{
   [TestClass]
   public class MaxValueAttributeFacts
   {
      [TestMethod]
      public void MaxValueAttribute_IsValid_ValueIsHigherThanMax_ShouldReturnFalse()
      {
         // arrange
         int maxValue = 5;
         int value = 6;
         var attribute = new MaxValueAttribute(maxValue);

         // act
         bool valid = attribute.IsValid(value);

         // assert
         Assert.IsFalse(valid);
      }

      [TestMethod]
      public void MaxValueAttribute_IsValid_ValueIsEqualToMax_ShouldReturnTrue()
      {
         // arrange
         int maxValue = 5;
         int value = 5;
         var attribute = new MaxValueAttribute(maxValue);

         // act
         bool valid = attribute.IsValid(value);

         // assert
         Assert.IsTrue(valid);
      }

      [TestMethod]
      public void MaxValueAttribute_IsValid_ValueIsSmallerThanMax_ShouldReturnTrue()
      {
         // arrange
         int maxValue = 5;
         int value = 4;
         var attribute = new MaxValueAttribute(maxValue);

         // act
         bool valid = attribute.IsValid(value);

         // assert
         Assert.IsTrue(valid);
      }
   }
}
