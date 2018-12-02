using System.Threading;
using System.Threading.Tasks;
using Ducode.Essentials.Async.Interfaces;

namespace Ducode.Essentials.Async
{
   /// <summary>
   /// A class that contains several async helper methods.
   /// </summary>
   /// <seealso cref="Ducode.Essentials.Async.Interfaces.IAsyncService" />
   public class AsyncService : IAsyncService
   {
      /// <summary>
      /// Adds a delay for a given amount of milliseconds.
      /// </summary>
      /// <param name="millis">The millis.</param>
      /// <returns>
      /// A task.
      /// </returns>
      public async Task DelayAsync(int millis)
      {
         await Task.Delay(millis);
      }

      /// <summary>
      /// Adds a delay for a given amount of milliseconds.
      /// </summary>
      /// <param name="millis">The millis.</param>
      public void Sleep(int millis)
      {
         Thread.Sleep(millis);
      }
   }
}
