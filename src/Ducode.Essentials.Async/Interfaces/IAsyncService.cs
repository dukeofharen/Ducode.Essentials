using System.Threading.Tasks;

namespace Ducode.Essentials.Async.Interfaces
{
   /// <summary>
   /// Describes a class that contains several async helper methods.
   /// </summary>
   public interface IAsyncService
   {
      /// <summary>
      /// Adds a delay for a given amount of milliseconds.
      /// </summary>
      /// <param name="millis">The millis.</param>
      /// <returns>A task.</returns>
      Task DelayAsync(int millis);
   }
}
