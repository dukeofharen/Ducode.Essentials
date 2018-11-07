using Ducode.Essentials.Files.Models;

namespace Ducode.Essentials.Files.Interfaces
{
   /// <summary>
   /// Describes a class that is used for retrieving the file caching settings.
   /// </summary>
   public interface IFileCachingSettingsProvider
   {
      /// <summary>
      /// Gets the file caching settings.
      /// </summary>
      /// <returns>The <see cref="FileCachingSettingsModel"/>.</returns>
      FileCachingSettingsModel GetFileCachingSettings();
   }
}
