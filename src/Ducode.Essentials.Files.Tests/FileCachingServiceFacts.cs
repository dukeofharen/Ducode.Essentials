using System;
using System.IO;
using System.Threading.Tasks;
using Ducode.Essentials.Async.Interfaces;
using Ducode.Essentials.Files.Interfaces;
using Ducode.Essentials.Files.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ducode.Essentials.Files.Tests
{
   [TestClass]
   public class FileCachingServiceFacts
   {
      private const string TempPath = @"C:\temp";
      private const string CacheFilename = "temp.json";
      private const string FileContents = @"{""name"": ""FileExists""}";

      private Mock<IAsyncService> _asyncServiceMock;
      private Mock<IFileCachingSettingsProvider> _fileCachingSettingsProviderMock;
      private Mock<IFileService> _fileServiceMock;
      private FileCachingService _service;

      [TestInitialize]
      public void Initialize()
      {
         _asyncServiceMock = new Mock<IAsyncService>();
         _fileCachingSettingsProviderMock = new Mock<IFileCachingSettingsProvider>();
         _fileServiceMock = new Mock<IFileService>();
         _service = new FileCachingService(
            _asyncServiceMock.Object,
             _fileCachingSettingsProviderMock.Object,
             _fileServiceMock.Object);
      }

      [TestCleanup]
      public void Cleanup()
      {
         _asyncServiceMock.VerifyAll();
         _fileCachingSettingsProviderMock.VerifyAll();
         _fileServiceMock.VerifyAll();
      }

      [TestMethod]
      public async Task FileCachingService_GetCachedFileAsync_FileExists_ShouldReturnDeserializedFile()
      {
         // arrange
         string expectedPath = Path.Combine(TempPath, CacheFilename);

         _fileCachingSettingsProviderMock
             .Setup(m => m.GetFileCachingSettings())
             .Returns(new FileCachingSettingsModel
             {
                TemporaryFolder = TempPath
             });

         _fileServiceMock
             .Setup(m => m.FileExists(expectedPath))
             .Returns(true);

         _fileServiceMock
             .Setup(m => m.ReadAllText(expectedPath))
             .Returns(FileContents);

         // act
         var result = await _service.GetCachedFileAsync<Thingy>(CacheFilename, null, null);

         // assert
         Assert.IsNotNull(result);
         Assert.AreEqual("FileExists", result.Name);
      }

      [TestMethod]
      public async Task FileCachingService_GetCachedFileAsync_FileExists_CheckAssertDelete()
      {
         // arrange
         string expectedPath = Path.Combine(TempPath, CacheFilename);
         var timeSpan = TimeSpan.FromHours(4);
         var fileWriteDateTime = DateTime.Now.Add(-timeSpan).AddSeconds(-1);

         _fileCachingSettingsProviderMock
             .Setup(m => m.GetFileCachingSettings())
             .Returns(new FileCachingSettingsModel
             {
                TemporaryFolder = TempPath
             });

         _fileServiceMock
             .Setup(m => m.FileExists(expectedPath))
             .Returns(true);

         _fileServiceMock
             .Setup(m => m.ReadAllText(expectedPath))
             .Returns(FileContents);

         _fileServiceMock
             .Setup(m => m.GetLastWriteTime(expectedPath))
             .Returns(fileWriteDateTime);

         // act
         var result = await _service.GetCachedFileAsync<Thingy>(CacheFilename, null, timeSpan);

         // assert
         Assert.IsNotNull(result);
         Assert.AreEqual("FileExists", result.Name);
         _fileServiceMock.Verify(m => m.DeleteFile(expectedPath), Times.Once);
      }

      [TestMethod]
      public async Task FileCachingService_GetCachedFileAsync_FileDoesntExist_ShouldCallFunctionAndWriteContentsToDisk()
      {
         // arrange
         string actualFileContents = null;
         string expectedPath = Path.Combine(TempPath, CacheFilename);

         var objectToSerialize = new Thingy("FileDoesntExist");

         _fileCachingSettingsProviderMock
             .Setup(m => m.GetFileCachingSettings())
             .Returns(new FileCachingSettingsModel
             {
                TemporaryFolder = TempPath
             });

         _fileServiceMock
             .Setup(m => m.FileExists(expectedPath))
             .Returns(false);

         _fileServiceMock
             .Setup(m => m.WriteAllText(expectedPath, It.IsAny<string>()))
             .Callback((string passedPass, string passedFileContents) =>
             {
                actualFileContents = passedFileContents;
             });

         // act
         var result = await _service.GetCachedFileAsync(CacheFilename, async () => await Task.FromResult(objectToSerialize), null);

         // assert
         Assert.AreEqual(objectToSerialize, result);
         Assert.IsNotNull(actualFileContents);
         Assert.IsTrue(actualFileContents.Contains(objectToSerialize.Name));
      }

      [TestMethod]
      public async Task FileCachingService_GetCachedFileAsync_FileDoesntExist_Exception_RetryAmountExceeded_ShouldThrowException()
      {
         // arrange
         string expectedPath = Path.Combine(TempPath, CacheFilename);

         var objectToSerialize = new Thingy("FileDoesntExist");

         _fileCachingSettingsProviderMock
             .Setup(m => m.GetFileCachingSettings())
             .Returns(new FileCachingSettingsModel
             {
                TemporaryFolder = TempPath
             });

         _fileServiceMock
             .Setup(m => m.FileExists(expectedPath))
             .Returns(false);

         _fileServiceMock
             .Setup(m => m.WriteAllText(expectedPath, It.IsAny<string>()))
             .Throws(new Exception());

         // act
         await Assert.ThrowsExceptionAsync<Exception>(() => _service.GetCachedFileAsync(CacheFilename, async () => await Task.FromResult(objectToSerialize), null));

         // assert
         _asyncServiceMock.Verify(m => m.Sleep(1000), Times.Exactly(2));
      }

      [TestMethod]
      public async Task FileCachingService_GetCachedFileAsync_FileDoesntExist_Exception_LastRetrySucceeds()
      {
         // arrange
         string expectedPath = Path.Combine(TempPath, CacheFilename);

         var objectToSerialize = new Thingy("FileDoesntExist");

         _fileCachingSettingsProviderMock
             .Setup(m => m.GetFileCachingSettings())
             .Returns(new FileCachingSettingsModel
             {
                TemporaryFolder = TempPath
             });

         _fileServiceMock
             .Setup(m => m.FileExists(expectedPath))
             .Returns(false);

         _fileServiceMock
            .SetupSequence(m => m.WriteAllText(expectedPath, It.IsAny<string>()))
            .Throws(new Exception())
            .Throws(new Exception())
            .Pass();

         // act
         var result = await _service.GetCachedFileAsync(CacheFilename, async () => await Task.FromResult(objectToSerialize), null);

         // assert
         Assert.AreEqual(objectToSerialize, result);
         _asyncServiceMock.Verify(m => m.Sleep(1000), Times.Exactly(2));
      }

      [TestMethod]
      public void FileCachingService_RemoveCachedFile_HappyFlow()
      {
         // arrange
         string expectedPath = Path.Combine(TempPath, CacheFilename);
         var timeSpan = TimeSpan.FromHours(4);
         var fileWriteDateTime = DateTime.Now.Add(-timeSpan).AddSeconds(-1);

         _fileServiceMock
             .Setup(m => m.GetLastWriteTime(expectedPath))
             .Returns(fileWriteDateTime);

         _fileCachingSettingsProviderMock
             .Setup(m => m.GetFileCachingSettings())
             .Returns(new FileCachingSettingsModel
             {
                TemporaryFolder = TempPath
             });

         _fileServiceMock
             .Setup(m => m.FileExists(expectedPath))
             .Returns(true);

         // act
         _service.RemoveCachedFile(CacheFilename);

         // assert
         _fileServiceMock.Verify(m => m.DeleteFile(expectedPath), Times.Once);
      }

      private class Thingy
      {
         public Thingy(string name)
         {
            Name = name;
         }

         public string Name { get; }
      }
   }
}
