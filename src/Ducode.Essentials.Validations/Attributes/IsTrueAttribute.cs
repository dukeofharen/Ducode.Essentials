using System;
using System.ComponentModel.DataAnnotations;

namespace Ducode.Essentials.Validations.Attributes
{
   /// <summary>
   /// A validation attribute for checking if a property is a boolean and true.
   /// </summary>
   public class IsTrueAttribute : ValidationAttribute
   {
      /// <summary>
      /// Returns true if validation is valid.
      /// </summary>
      /// <param name="value">The value of the object to validate.</param>
      /// <returns>
      /// true if the specified value is valid; otherwise, false.
      /// </returns>
      /// <exception cref="System.InvalidOperationException">can only be used on boolean properties.</exception>
      public override bool IsValid(object value)
      {
         if (value == null)
         {
            return false;
         }

         if (value.GetType() != typeof(bool))
         {
            throw new InvalidOperationException("can only be used on boolean properties.");
         }

         return (bool)value;
      }
   }
}
