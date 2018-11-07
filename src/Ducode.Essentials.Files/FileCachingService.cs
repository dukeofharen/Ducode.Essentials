using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Ducode.Essentials.Files.Interfaces;
using Newtonsoft.Json;

namespace Ducode.Essentials.Files
{
   /// <summary>
   /// A class used for storing objects on disk as cache.
   /// </summary>
   /// <seealso cref="Ducode.Essentials.Files.Interfaces.IFileCachingService" />
   public class FileCachingService : IFileCachingService
   {
      private readonly IFileCachingSettingsProvider _fileCachingSettingsProvider;
      private readonly IFileService _fileService;

      /// <summary>
      /// Initializes a new instance of the <see cref="FileCachingService"/> class.
      /// </summary>
      /// <param name="fileCachingSettingsProvider">The file caching settings provider.</param>
      /// <param name="fileService">The file service.</param>
      public FileCachingService(
          IFileCachingSettingsProvider fileCachingSettingsProvider,
          IFileService fileService)
      {
         _fileCachingSettingsProvider = fileCachingSettingsProvider;
         _fileService = fileService;
      }

      /// <summary>
      /// Gets or adds a cached item to disk.
      /// </summary>
      /// <typeparam name="TObject">The type of the object.</typeparam>
      /// <param name="filename">The filename.</param>
      /// <param name="fileNotExistsFunc">The file not exists function.</param>
      /// <param name="validSpan">The valid span.</param>
      /// <returns>
      /// The cached item.
      /// </returns>
      public async Task<TObject> GetCachedFileAsync<TObject>(string filename, Func<Task<TObject>> fileNotExistsFunc, TimeSpan? validSpan = default(TimeSpan?))
      {
         AssertDeleteFile(filename, validSpan);
         if (FileExists(filename))
         {
            return JsonConvert.DeserializeObject<TObject>(GetFileText(filename));
         }

         TObject obj = await fileNotExistsFunc();
         SaveFileText(filename, JsonConvert.SerializeObject(obj));
         return obj;
      }

      /// <summary>
      /// Removes the cached file from disk.
      /// </summary>
      /// <param name="filename">The filename.</param>
      public void RemoveCachedFile(string filename)
      {
         AssertDeleteFile(filename, TimeSpan.MinValue);
      }

      private string GetFileText(string filename)
      {
         if (!FileExists(filename))
         {
            return null;
         }

         var tempPath = GetTempPath();
         return _fileService.ReadAllText(Path.Combine(tempPath, filename));
      }

      private void AssertDeleteFile(string filename, TimeSpan? validSpan = null)
      {
         if (validSpan.HasValue)
         {
            var path = Path.Combine(GetTempPath(), filename);
            if (_fileService.FileExists(path))
            {
               DateTime lastUpdate = _fileService.GetLastWriteTime(path);
               if (DateTime.Now - lastUpdate > validSpan.Value)
               {
                  _fileService.DeleteFile(path);
               }
            }
         }
      }

      private string GetTempPath()
      {
         var settings = _fileCachingSettingsProvider.GetFileCachingSettings();
         return settings.TemporaryFolder;
      }

      private bool FileExists(string filename)
      {
         var tempPath = GetTempPath();
         return _fileService.FileExists(Path.Combine(tempPath, filename));
      }

      private void SaveFileText(string filename, string text)
      {
         var tempPath = GetTempPath();
         _fileService.WriteAllText(Path.Combine(tempPath, filename), text);
      }
   }
}
