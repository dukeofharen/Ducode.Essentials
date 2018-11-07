using System;
using System.Threading.Tasks;

namespace Ducode.Essentials.Files.Interfaces
{
   /// <summary>
   /// Describes a class used for storing objects on disk as cache.
   /// </summary>
   public interface IFileCachingService
   {
      /// <summary>
      /// Gets or adds a cached item to disk.
      /// </summary>
      /// <typeparam name="TObject">The type of the object.</typeparam>
      /// <param name="filename">The filename.</param>
      /// <param name="fileNotExistsFunc">The file not exists function.</param>
      /// <param name="validSpan">The valid span.</param>
      /// <returns>The cached item.</returns>
      Task<TObject> GetCachedFileAsync<TObject>(string filename, Func<Task<TObject>> fileNotExistsFunc, TimeSpan? validSpan = null);

      /// <summary>
      /// Removes the cached file from disk.
      /// </summary>
      /// <param name="filename">The filename.</param>
      void RemoveCachedFile(string filename);
   }
}
