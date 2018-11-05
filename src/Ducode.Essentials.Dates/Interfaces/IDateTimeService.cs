using System;

namespace Ducode.Essentials.Dates.Interfaces
{
   /// <summary>
   /// Describes a class that can be used to perform several DateTime related tasks.
   /// </summary>
   public interface IDateTimeService
   {
      /// <summary>
      /// Returns the current local date and time.
      /// </summary>
      /// <value>
      /// The current local date and time.
      /// </value>
      DateTime Now { get; }

      /// <summary>
      /// Returns the current UTC date and time.
      /// </summary>
      /// <value>
      /// The current UTC date and time.
      /// </value>
      DateTime UtcNow { get; }
   }
}
