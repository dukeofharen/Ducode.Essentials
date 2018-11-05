using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Ducode.Essentials.Web.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ducode.Essentials.NlPostcode.Tests
{
   [TestClass]
   public class PostcodeServiceFacts
   {

      private Mock<IWebService> _webServiceMock;
      private PostcodeService _service;

      [TestInitialize]
      public void Initialize()
      {
         _webServiceMock = new Mock<IWebService>();
         _service = new PostcodeService(_webServiceMock.Object);
      }

      [TestCleanup]
      public void Cleanup()
      {
         _webServiceMock.VerifyAll();
      }

      [TestMethod]
      public async Task PostcodeService_GetPostcodeAsync_PostcodeIsNull_ShouldThrowArgumentNullException()
      {
         // act / assert
         await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _service.GetPostcodeAsync(null));
      }

      [TestMethod]
      public async Task PostcodeService_GetPostcodeAsync_LengthNot6_ShouldThrowArgumentException()
      {
         // arrange
         string postcode = "9752V";

         // act / assert
         await Assert.ThrowsExceptionAsync<ArgumentException>(() => _service.GetPostcodeAsync(postcode));
      }

      [TestMethod]
      public async Task PostcodeService_GetPostcodeAsync_EmptyResponse_ShouldThrowInvalidOperationException()
      {
         // arrange
         string postcode = "9752CA";

         _webServiceMock
            .Setup(m => m.GetAsync(string.Format(NlPostcodeConstants.PdokSuggestUrl, postcode)))
            .ReturnsAsync(new HttpResponseMessage
            {
               StatusCode = HttpStatusCode.OK,
               Content = new StringContent(PostcodeServiceFactsResources.EmptyResponse)
            });

         // act
         var exception = await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _service.GetPostcodeAsync(postcode));

         // assert
         Assert.AreEqual("Invalid response object", exception.Message);
      }

      [TestMethod]
      public async Task PostcodeService_GetPostcodeAsync_NoDocsObject_ShouldReturnFalse()
      {
         // arrange
         string postcode = "9752CA";

         _webServiceMock
            .Setup(m => m.GetAsync(string.Format(NlPostcodeConstants.PdokSuggestUrl, postcode)))
            .ReturnsAsync(new HttpResponseMessage
            {
               StatusCode = HttpStatusCode.OK,
               Content = new StringContent(PostcodeServiceFactsResources.DocsMissingResponse)
            });

         // act
         var result = await _service.GetPostcodeAsync(postcode);

         // assert
         Assert.IsFalse(result.Success);
      }

      [TestMethod]
      public async Task PostcodeService_GetPostcodeAsync_NoPostcodeObject_ShouldThrowInvalidOperationException()
      {
         // arrange
         string postcode = "9752CA";

         _webServiceMock
            .Setup(m => m.GetAsync(string.Format(NlPostcodeConstants.PdokSuggestUrl, postcode)))
            .ReturnsAsync(new HttpResponseMessage
            {
               StatusCode = HttpStatusCode.OK,
               Content = new StringContent(PostcodeServiceFactsResources.PostcodeMissingResponse)
            });

         // act
         var result = await _service.GetPostcodeAsync(postcode);

         // assert
         Assert.IsFalse(result.Success);
      }

      [TestMethod]
      public async Task PostcodeService_GetPostcodeAsync_WeergavenaamEmpty_ShouldThrowInvalidOperationException()
      {
         // arrange
         string postcode = "9752CA";

         _webServiceMock
            .Setup(m => m.GetAsync(string.Format(NlPostcodeConstants.PdokSuggestUrl, postcode)))
            .ReturnsAsync(new HttpResponseMessage
            {
               StatusCode = HttpStatusCode.OK,
               Content = new StringContent(PostcodeServiceFactsResources.WeergavenaamEmptyResponse)
            });

         // act
         var exception = await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _service.GetPostcodeAsync(postcode));

         // assert
         Assert.AreEqual("weergavenaam is empty", exception.Message);
      }

      [TestMethod]
      public async Task PostcodeService_GetPostcodeAsync_PostcodeRegexFails_ShouldThrowInvalidOperationException()
      {
         // arrange
         string postcode = "9752CA";

         _webServiceMock
            .Setup(m => m.GetAsync(string.Format(NlPostcodeConstants.PdokSuggestUrl, postcode)))
            .ReturnsAsync(new HttpResponseMessage
            {
               StatusCode = HttpStatusCode.OK,
               Content = new StringContent(PostcodeServiceFactsResources.PostcodeRegexFailsResponse)
            });

         // act
         var exception = await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _service.GetPostcodeAsync(postcode));

         // assert
         Assert.AreEqual("Length of matches not 4", exception.Message);
      }

      [TestMethod]
      public async Task PostcodeService_GetPostcodeAsync_HappyFlow()
      {
         // arrange
         string postcode = "9752CA";

         _webServiceMock
            .Setup(m => m.GetAsync(string.Format(NlPostcodeConstants.PdokSuggestUrl, postcode)))
            .ReturnsAsync(new HttpResponseMessage
            {
               StatusCode = HttpStatusCode.OK,
               Content = new StringContent(PostcodeServiceFactsResources.SuccessResponse)
            });

         // act
         var result = await _service.GetPostcodeAsync(postcode);

         // assert
         Assert.IsTrue(result.Success);
         Assert.AreEqual("9752CA", result.Resource.Postcode);
         Assert.AreEqual("Rijksstraatweg", result.Resource.Street);
         Assert.AreEqual("Haren Gn", result.Resource.Town);
      }

      [TestMethod]
      public async Task PostcodeService_GetPostcodeAsync_HappyFlow_SpacesAndLowerCase()
      {
         // arrange
         string postcode = "9752 ca";

         _webServiceMock
            .Setup(m => m.GetAsync(string.Format(NlPostcodeConstants.PdokSuggestUrl, "9752CA")))
            .ReturnsAsync(new HttpResponseMessage
            {
               StatusCode = HttpStatusCode.OK,
               Content = new StringContent(PostcodeServiceFactsResources.SuccessResponse)
            });

         // act
         var result = await _service.GetPostcodeAsync(postcode);

         // assert
         Assert.IsTrue(result.Success);
         Assert.AreEqual("9752CA", result.Resource.Postcode);
         Assert.AreEqual("Rijksstraatweg", result.Resource.Street);
         Assert.AreEqual("Haren Gn", result.Resource.Town);
      }
   }
}
