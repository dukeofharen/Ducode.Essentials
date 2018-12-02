using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Ducode.Essentials.Async.Interfaces;
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
      private const int RetryMillis = 1000;
      private const int NumberOfRetries = 3;
      private readonly IAsyncService _asyncService;
      private readonly IFileCachingSettingsProvider _fileCachingSettingsProvider;
      private readonly IFileService _fileService;

      /// <summary>
      /// Initializes a new instance of the <see cref="FileCachingService" /> class.
      /// </summary>
      /// <param name="asyncService">The asynchronous service.</param>
      /// <param name="fileCachingSettingsProvider">The file caching settings provider.</param>
      /// <param name="fileService">The file service.</param>
      public FileCachingService(
         IAsyncService asyncService,
          IFileCachingSettingsProvider fileCachingSettingsProvider,
          IFileService fileService)
      {
         _asyncService = asyncService;
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

         var obj = await fileNotExistsFunc();
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

         return ReadAllText(filename);
      }

      private void AssertDeleteFile(string filename, TimeSpan? validSpan = null)
      {
         if (validSpan.HasValue)
         {
            if (FileExists(filename))
            {
               var lastUpdate = GetLastWriteDateTime(filename);
               if (DateTime.Now - lastUpdate > validSpan.Value)
               {
                  DeleteFile(filename);
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
         Func<bool> func = () => _fileService.FileExists(Path.Combine(tempPath, filename));
         return Retry(func);
      }

      private void SaveFileText(string filename, string text)
      {
         var tempPath = GetTempPath();
         Action action = () => _fileService.WriteAllText(Path.Combine(tempPath, filename), text);
         Retry(action);
      }

      private void DeleteFile(string filename)
      {
         var tempPath = GetTempPath();
         Action action = () => _fileService.DeleteFile(Path.Combine(tempPath, filename));
         Retry(action);
      }

      private string ReadAllText(string filename)
      {
         var tempPath = GetTempPath();
         Func<string> func = () => _fileService.ReadAllText(Path.Combine(tempPath, filename));
         return Retry(func);
      }

      private DateTime GetLastWriteDateTime(string filename)
      {
         var tempPath = GetTempPath();
         Func<DateTime> func = () => _fileService.GetLastWriteTime(Path.Combine(tempPath, filename));
         return Retry(func);
      }

      private void Retry(Action action)
      {
         int counter = 0;
         while (true)
         {
            try
            {
               action();
               break;
            }
            catch (Exception)
            {
               counter++;
               if (counter >= NumberOfRetries)
               {
                  throw;
               }

               _asyncService.Sleep(RetryMillis);
            }
         }
      }

      private TResult Retry<TResult>(Func<TResult> func)
      {
         int counter = 0;
         while (true)
         {
            try
            {
               return func();
            }
            catch (Exception)
            {
               counter++;
               if (counter > NumberOfRetries)
               {
                  throw;
               }

               _asyncService.Sleep(RetryMillis);
            }
         }
      }
   }
}
