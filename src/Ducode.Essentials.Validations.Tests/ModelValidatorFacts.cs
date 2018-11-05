using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ducode.Essentials.Validations.Tests
{
   [TestClass]
   public class ModelValidatorFacts
   {
      private ModelValidator _validator;

      [TestInitialize]
      public void Initialize()
      {
         _validator = new ModelValidator();
      }

      [TestMethod]
      public void ModelValidator_ValidateModel_ValidationFailed()
      {
         // arrange
         var model = new TestModel
         {
            Value = null
         };

         // act
         var results = _validator.ValidateModel(model);

         // assert
         Assert.AreEqual(1, results.Count());
      }

      [TestMethod]
      public void ModelValidator_ValidateModel_ValidationPassed()
      {
         // arrange
         var model = new TestModel
         {
            Value = "filled in"
         };

         // act
         var results = _validator.ValidateModel(model);

         // assert
         Assert.AreEqual(0, results.Count());
      }

      public class TestModel
      {
         [Required]
         public string Value { get; set; }
      }
   }
}
