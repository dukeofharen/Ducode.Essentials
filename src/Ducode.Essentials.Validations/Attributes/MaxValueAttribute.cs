using System.ComponentModel.DataAnnotations;

namespace Ducode.Essentials.Validations.Attributes
{
   /// <summary>
   /// An attribute for checking if a value is smaller than a given value.
   /// </summary>
   public class MaxValueAttribute : ValidationAttribute
   {
      private readonly int _maxValue;

      /// <summary>
      /// Initializes a new instance of the <see cref="MaxValueAttribute"/> class.
      /// </summary>
      /// <param name="maxValue">The maximum value.</param>
      public MaxValueAttribute(int maxValue)
      {
         _maxValue = maxValue;
      }

      /// <summary>
      /// Returns true if the validation is valid.
      /// </summary>
      /// <param name="value">The value of the object to validate.</param>
      /// <returns>
      /// true if the specified value is valid; otherwise, false.
      /// </returns>
      public override bool IsValid(object value)
      {
         return (int?)value <= _maxValue;
      }
   }
}
