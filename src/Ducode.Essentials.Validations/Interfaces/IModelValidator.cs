using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ducode.Essentials.Validations.Interfaces
{
   /// <summary>
   /// Describes a class used for validating specific models.
   /// </summary>
   public interface IModelValidator
   {
      /// <summary>
      /// Validates a model.
      /// </summary>
      /// <param name="model">The model.</param>
      /// <returns>A list of validation results. Empty if validation passed.</returns>
      IEnumerable<ValidationResult> ValidateModel(object model);
   }
}
