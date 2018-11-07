using System;

namespace Ducode.Essentials.Files.Interfaces
{
   /// <summary>
   /// Descibrs a class for working with files.
   /// </summary>
   public interface IFileService
   {
      /// <summary>
      /// Reads all bytes of a file.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <returns>All file bytes.</returns>
      byte[] ReadAllBytes(string path);

      /// <summary>
      /// Reads all text of a file.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <returns>All file text.</returns>
      string ReadAllText(string path);

      /// <summary>
      /// Writes all bytes to a file.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <param name="contents">The contents.</param>
      void WriteAllBytes(string path, byte[] contents);

      /// <summary>
      /// Writes all text to a file.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <param name="contents">The contents.</param>
      void WriteAllText(string path, string contents);

      /// <summary>
      /// Checks whether a given file exists.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <returns>True if file exists; false otherwise.</returns>
      bool FileExists(string path);

      /// <summary>
      /// Checks whether a given directory exists.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <returns>True if directory exists; false otherwise.</returns>
      bool DirectoryExists(string path);

      /// <summary>
      /// Creates a directory.
      /// </summary>
      /// <param name="path">The path.</param>
      void CreateDirectory(string path);

      /// <summary>
      /// Gets the systems temporary path.
      /// </summary>
      /// <returns>The temporary path.</returns>
      string GetTempPath();

      /// <summary>
      /// Deletes a file.
      /// </summary>
      /// <param name="path">The path.</param>
      void DeleteFile(string path);

      /// <summary>
      /// Gets the last write time of a file.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <returns>The last file write time.</returns>
      DateTime GetLastWriteTime(string path);

      /// <summary>
      /// Determines whether the specified path is a directory.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <returns>
      ///   <c>true</c> if the specified path is directory; otherwise, <c>false</c>.
      /// </returns>
      bool IsDirectory(string path);

      /// <summary>
      /// Lists all files from a directory.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <param name="searchPattern">The search pattern.</param>
      /// <returns>A list with file paths.</returns>
      string[] GetFiles(string path, string searchPattern);

      /// <summary>
      /// Gets the current directory.
      /// </summary>
      /// <returns>The current directory path.</returns>
      string GetCurrentDirectory();

      /// <summary>
      /// Gets the last modication date and time of a path.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <returns>The last modification date and time.</returns>
      DateTime GetModicationDateTime(string path);
   }
}
