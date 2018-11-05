using System;
using Ducode.Essentials.Dates.Interfaces;

namespace Ducode.Essentials.Dates
{
   /// <summary>
   /// A class that can be used to perform several DateTime related tasks.
   /// </summary>
   /// <seealso cref="Ducode.Essentials.Dates.Interfaces.IDateTimeService" />
   public class DateTimeService : IDateTimeService
   {
      /// <summary>
      /// Returns the current local date and time.
      /// </summary>
      /// <value>
      /// The current local date and time.
      /// </value>
      public DateTime Now => DateTime.Now;

      /// <summary>
      /// Returns the current UTC date and time.
      /// </summary>
      /// <value>
      /// The current UTC date and time.
      /// </value>
      public DateTime UtcNow => DateTime.UtcNow;
   }
}
