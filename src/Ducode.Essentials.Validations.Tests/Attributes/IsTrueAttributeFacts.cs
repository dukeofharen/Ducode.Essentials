using System;
using Ducode.Essentials.Validations.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ducode.Essentials.Validations.Tests.Attributes
{
   [TestClass]
   public class IsTrueAttributeFacts
   {
      private readonly IsTrueAttribute _attribute = new IsTrueAttribute();

      [TestMethod]
      public void IsTrueAttribute_IsValid_ValueIsNull_ShouldReturnFalse()
      {
         // act
         bool result = _attribute.IsValid(null);

         // assert
         Assert.IsFalse(result);
      }

      [TestMethod]
      public void IsTrueAttribute_IsValid_ValueIsNotBoolean_ShouldThrowInvalidOperationException()
      {
         // act / assert
         Assert.ThrowsException<InvalidOperationException>(() => _attribute.IsValid(100));
      }

      [TestMethod]
      public void IsTrueAttribute_IsValid_HappyFlow()
      {
         // act
         bool result = _attribute.IsValid(true);

         // assert
         Assert.IsTrue(result);
      }
   }
}
