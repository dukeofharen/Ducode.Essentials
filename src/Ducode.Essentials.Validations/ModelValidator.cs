using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ducode.Essentials.Validations.Interfaces;

namespace Ducode.Essentials.Validations
{
   /// <summary>
   /// A class used for validating specific models.
   /// </summary>
   /// <seealso cref="Ducode.Essentials.Validations.Interfaces.IModelValidator" />
   public class ModelValidator : IModelValidator
   {
      /// <summary>
      /// Validates a model.
      /// </summary>
      /// <param name="model">The model.</param>
      /// <returns>
      /// A list of validation results. Empty if validation passed.
      /// </returns>
      public IEnumerable<ValidationResult> ValidateModel(object model)
      {
         var context = new ValidationContext(model, null, null);
         var results = new List<ValidationResult>();
         Validator.TryValidateObject(model, context, results, true);
         return results;
      }
   }
}
