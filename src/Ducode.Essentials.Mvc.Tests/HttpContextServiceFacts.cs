using Ducode.Essentials.Mvc.Interfaces;
using Ducode.Essentials.Mvc.TestUtilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
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

         _service = new HttpContextService(
            new ClientIpResolver(httpContextAccessorMock.Object),
            httpContextAccessorMock.Object);
      }

      [TestCleanup]
      public void Cleanup()
      {
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

      [TestMethod]
      public void HttpContextService_Path_HappyFlow()
      {
         // arrange
         string path = "/page";
         _mockHttpContext.SetRequestPath(path);

         // act
         string result = _service.Path;

         // assert
         Assert.AreEqual(path, result);
      }

      [TestMethod]
      public void HttpContextService_FullPath_HappyFlow()
      {
         // arrange
         string path = "/page";
         string queryString = "?value=1&value2=3";
         _mockHttpContext.SetRequestPath(path);
         _mockHttpContext.SetQueryString(queryString);

         // act
         string result = _service.FullPath;

         // assert
         Assert.AreEqual($"{path}{queryString}", result);
      }

      [TestMethod]
      public void HttpContextService_GetHost_NoXForwardedHostHeaderSet_ShouldReturnOriginalHost()
      {
         // arrange
         string host = "ducode.org";

         _mockHttpContext.SetHost(host);
         _mockHttpContext.SetIp("11.22.33.44");

         // act
         string result = _service.GetHost();

         // assert
         Assert.AreEqual(host, result);
      }

      [TestMethod]
      public void HttpContextService_GetHost_XForwardedHostHeaderSet_IpNotLocalhost_ShouldReturnOriginalHost()
      {
         // arrange
         string originalHost = "ducode.org";
         string headerHost = "ducode2.org";
         string ip = "11.22.33.44";

         _mockHttpContext.SetHost(originalHost);
         _mockHttpContext.SetIp(ip);
         _mockHttpContext.Request.Headers.Add("X-Forwarded-Host", new StringValues(headerHost));

         // act
         string result = _service.GetHost();

         // assert
         Assert.AreEqual(originalHost, result);
      }

      [TestMethod]
      public void HttpContextService_GetHost_XForwardedHostHeaderSet_IpLocalhost_ShouldReturnHeaderHost()
      {
         // arrange
         string originalHost = "ducode.org";
         string headerHost = "ducode2.org";
         string ip = "127.0.0.1";

         _mockHttpContext.SetHost(originalHost);
         _mockHttpContext.SetIp(ip);
         _mockHttpContext.Request.Headers.Add("X-Forwarded-Host", new StringValues(headerHost));

         // act
         string result = _service.GetHost();

         // assert
         Assert.AreEqual(headerHost, result);
      }

      [TestMethod]
      public void HttpContextService_IsHttps_NoXForwardedProtoHeaderSet_ShouldReturnOriginalIsHttps()
      {
         // arrange
         _mockHttpContext.SetHttps(true);
         _mockHttpContext.SetIp("11.22.33.44");

         // act
         bool result = _service.IsHttps();

         // assert
         Assert.IsTrue(result);
      }

      [TestMethod]
      public void HttpContextService_IsHttps_XForwardedProtoHeaderSet_IpNotLocalhost_ShouldReturnOriginalIsHttps()
      {
         // arrange
         string ip = "11.22.33.44";

         _mockHttpContext.SetHttps(true);
         _mockHttpContext.Request.Headers.Add("X-Forwarded-Proto", new StringValues("http"));
         _mockHttpContext.SetIp(ip);

         // act
         bool result = _service.IsHttps();

         // assert
         Assert.IsTrue(result);
      }

      [TestMethod]
      public void HttpContextService_IsHttps_XForwardedProtoHeaderSet_IpLocalhost_ShouldReturnHeaderIsHttps()
      {
         // arrange
         string ip = "127.0.0.1";

         _mockHttpContext.SetHttps(true);
         _mockHttpContext.Request.Headers.Add("X-Forwarded-Proto", new StringValues("http"));
         _mockHttpContext.SetIp(ip);

         // act
         bool result = _service.IsHttps();

         // assert
         Assert.IsFalse(result);
      }
   }
}
