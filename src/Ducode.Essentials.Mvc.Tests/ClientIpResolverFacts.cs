using Ducode.Essentials.Mvc.TestUtilities;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ducode.Essentials.Mvc.Tests
{
   [TestClass]
   public class ClientIpResolverFacts
   {
      private MockHttpContext _mockHttpContext;
      private ClientIpResolver _service;

      [TestInitialize]
      public void Initialize()
      {
         _mockHttpContext = new MockHttpContext();

         var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
         httpContextAccessorMock
            .Setup(m => m.HttpContext)
            .Returns(_mockHttpContext);

         _service = new ClientIpResolver(httpContextAccessorMock.Object);
      }

      [TestMethod]
      public void ClientIpResolver_GetClientIp_NoXForwardedForHeaderSent_ShouldReturnActualIp()
      {
         // arrange
         string ip = "127.0.0.1";

         _mockHttpContext.SetIp(ip);

         // act
         string result = _service.GetClientIp();

         // assert
         Assert.AreEqual(ip, result);
      }

      [TestMethod]
      public void ClientIpResolver_GetClientIp_XForwardedForHeaderSent_ButActualIpIsNotLocalhost_ShouldReturnActualIp()
      {
         // arrange
         string ip = "1.2.3.4";

         _mockHttpContext.SetIp(ip);
         _mockHttpContext.Request.Headers.Add("X-Forwarded-For", "1.1.2.2, 4.4.5.5");

         // act
         string result = _service.GetClientIp();

         // assert
         Assert.AreEqual(ip, result);
      }

      [TestMethod]
      public void ClientIpResolver_GetClientIp_XForwardedForHeaderSent_ActualIpIsLocalhost_ShouldReturnForwardedIp()
      {
         // arrange
         string ip = "127.0.0.1";

         _mockHttpContext.SetIp(ip);
         _mockHttpContext.Request.Headers.Add("X-Forwarded-For", "1.1.2.2, 4.4.5.5");

         // act
         string result = _service.GetClientIp();

         // assert
         Assert.AreEqual("1.1.2.2", result);
      }

      [TestMethod]
      public void ClientIpResolver_GetClientIp_XForwardedForHeaderSent_ActualIpIsLocalhost_ShouldReturnForwardedIp_CaseInsensitive()
      {
         // arrange
         string ip = "127.0.0.1";

         _mockHttpContext.SetIp(ip);
         _mockHttpContext.Request.Headers.Add("X-FORWARDeD-FOR", "1.1.2.2, 4.4.5.5");

         // act
         string result = _service.GetClientIp();

         // assert
         Assert.AreEqual("1.1.2.2", result);
      }

      [TestMethod]
      public void ClientIpResolver_GetClientIp_XForwardedForHeaderSent_ActualIpIsLocalhost_ShouldReturnForwardedIp_Ipv6()
      {
         // arrange
         string ip = "::1";

         _mockHttpContext.SetIp(ip);
         _mockHttpContext.Request.Headers.Add("X-Forwarded-For", "1.1.2.2, 4.4.5.5");

         // act
         string result = _service.GetClientIp();

         // assert
         Assert.AreEqual("1.1.2.2", result);
      }
   }
}
