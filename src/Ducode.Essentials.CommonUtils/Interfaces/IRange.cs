namespace Ducode.Essentials.CommonUtils.Interfaces
{
   /// <summary>
   /// Describes a class that is a range.
   /// Source: https://stackoverflow.com/questions/4781611/how-to-know-if-a-datetime-is-between-a-daterange-in-c-sharp
   /// </summary>
   /// <typeparam name="T">The class the range is applied to.</typeparam>
   public interface IRange<T>
   {
      /// <summary>
      /// The start object.
      /// </summary>
      T Start { get; }

      /// <summary>
      /// The end object.
      /// </summary>
      T End { get; }

      /// <summary>
      /// Checks whether the given object falls within the range.
      /// </summary>
      /// <param name="value">The value.</param>
      /// <returns>True if in range; false otherwise.</returns>
      bool Includes(T value);

      /// <summary>
      /// Checks whether the given range falls within the range.
      /// </summary>
      /// <param name="range">The range.</param>
      /// <returns>True if in range; false otherwise.</returns>
      bool Includes(IRange<T> range);
   }
}
