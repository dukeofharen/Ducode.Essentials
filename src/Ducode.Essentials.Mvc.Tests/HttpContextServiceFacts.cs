using Ducode.Essentials.Mvc.TestUtilities;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ducode.Essentials.Mvc.Tests
{
   [TestClass]
   public class HttpContextServiceFacts
   {
      private MockHttpContext _mockHttpContext;
      private HttpContextService _service;

      [TestInitialize]
      public void Initialize()
      {
         _mockHttpContext = new MockHttpContext();

         var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
         httpContextAccessorMock
            .Setup(m => m.HttpContext)
            .Returns(_mockHttpContext);

         _service = new HttpContextService(httpContextAccessorMock.Object);
      }

      [TestMethod]
      public void HttpContextService_Method_HappyFlow()
      {
         // arrange
         string method = "GET";
         _mockHttpContext.SetRequestMethod(method);

         // act
         string result = _service.Method;

         // assert
         Assert.AreEqual(method, result);
      }
   }
}
