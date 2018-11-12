using System.ComponentModel.DataAnnotations;

namespace Ducode.Essentials.Validations.Attributes
{
   /// <summary>
   /// An attribute for checking if a value is larger than a given value.
   /// </summary>
   public class MinValueAttribute : ValidationAttribute
   {
      private readonly int _minValue;

      /// <summary>
      /// Initializes a new instance of the <see cref="MinValueAttribute"/> class.
      /// </summary>
      /// <param name="minValue">The minimum value.</param>
      public MinValueAttribute(int minValue)
      {
         _minValue = minValue;
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
         return (int?)value >= _minValue;
      }
   }
}
