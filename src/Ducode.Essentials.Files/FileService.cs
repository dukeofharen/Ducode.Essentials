﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ducode.Essentials.Files.Interfaces;

namespace Ducode.Essentials.Files
{
   /// <summary>
   /// A class for working with files.
   /// </summary>
   /// <seealso cref="Ducode.Essentials.Files.Interfaces.IFileService" />
   public class FileService : IFileService
   {
      /// <summary>
      /// Reads all bytes of a file.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <returns>
      /// All file bytes.
      /// </returns>
      public byte[] ReadAllBytes(string path)
      {
         return File.ReadAllBytes(path);
      }

      /// <summary>
      /// Reads all text of a file.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <returns>
      /// All file text.
      /// </returns>
      public string ReadAllText(string path)
      {
         return File.ReadAllText(path);
      }

      /// <summary>
      /// Writes all bytes to a file.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <param name="contents">The contents.</param>
      public void WriteAllBytes(string path, byte[] contents)
      {
         File.WriteAllBytes(path, contents);
      }

      /// <summary>
      /// Writes all text to a file.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <param name="contents">The contents.</param>
      public void WriteAllText(string path, string contents)
      {
         File.WriteAllText(path, contents);
      }

      /// <summary>
      /// Checks whether a given file exists.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <returns>
      /// True if file exists; false otherwise.
      /// </returns>
      public bool FileExists(string path)
      {
         return File.Exists(path);
      }

      /// <summary>
      /// Checks whether a given directory exists.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <returns>
      /// True if directory exists; false otherwise.
      /// </returns>
      public bool DirectoryExists(string path)
      {
         return Directory.Exists(path);
      }

      /// <summary>
      /// Creates a directory.
      /// </summary>
      /// <param name="path">The path.</param>
      public void CreateDirectory(string path)
      {
         Directory.CreateDirectory(path);
      }

      /// <summary>
      /// Gets the systems temporary path.
      /// </summary>
      /// <returns>
      /// The temporary path.
      /// </returns>
      public string GetTempPath()
      {
         return Path.GetTempPath();
      }

      /// <summary>
      /// Deletes a file.
      /// </summary>
      /// <param name="path">The path.</param>
      public void DeleteFile(string path)
      {
         File.Delete(path);
      }

      /// <summary>
      /// Gets the last write time of a file.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <returns>
      /// The last file write time.
      /// </returns>
      public DateTime GetLastWriteTime(string path)
      {
         return File.GetLastWriteTime(path);
      }
   }
}
