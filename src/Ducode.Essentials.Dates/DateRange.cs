using System;
using Ducode.Essentials.CommonUtils.Interfaces;

namespace Ducode.Essentials.Dates
{
   /// <summary>
   /// A struct that is used for date range operations.
   /// Source: https://stackoverflow.com/questions/4781611/how-to-know-if-a-datetime-is-between-a-daterange-in-c-sharp
   /// </summary>
   public struct DateRange : IRange<DateTime>
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="DateRange"/> struct.
      /// </summary>
      /// <param name="start">The start.</param>
      /// <param name="end">The end.</param>
      public DateRange(DateTime start, DateTime end)
      {
         Start = start;
         End = end;
      }

      /// <summary>
      /// Gets the start date.
      /// </summary>
      public DateTime Start { get; private set; }

      /// <summary>
      /// Gets the end date.
      /// </summary>
      public DateTime End { get; private set; }

      /// <summary>
      /// Checks whether the given object falls within the range.
      /// </summary>
      /// <param name="value">The value.</param>
      /// <returns>
      /// True if in range; false otherwise.
      /// </returns>
      public bool Includes(DateTime value)
      {
         return (Start <= value) && (value <= End);
      }

      /// <summary>
      /// Checks whether the given range falls within the range.
      /// </summary>
      /// <param name="range">The range.</param>
      /// <returns>
      /// True if in range; false otherwise.
      /// </returns>
      public bool Includes(IRange<DateTime> range)
      {
         return (Start <= range.Start) && (range.End <= End);
      }
   }
}
