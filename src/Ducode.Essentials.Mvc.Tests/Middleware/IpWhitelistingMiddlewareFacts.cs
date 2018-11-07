using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Ducode.Essentials.Mvc.Interfaces;
using Ducode.Essentials.Mvc.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ducode.Essentials.Mvc.Tests.Middleware
{
   [TestClass]
   public class IpWhitelistingMiddlewareFacts
   {
      private Mock<IClientIpResolver> _clientIpResolverMock;
      private Mock<IIpWhitelistingSettingsProvider> _ipWhitelistingSettingsProviderMock;
      private TestServer _testServer;
      private HttpClient _httpClient;

      [TestInitialize]
      public void Initialize()
      {
         _clientIpResolverMock = new Mock<IClientIpResolver>();
         _ipWhitelistingSettingsProviderMock = new Mock<IIpWhitelistingSettingsProvider>();
         _testServer = new TestServer(
            new WebHostBuilder()
            .ConfigureServices(ConfigureServices)
            .Configure(Configure));
         _httpClient = _testServer.CreateClient();
      }

      [TestCleanup]
      public void Cleanup()
      {
         _clientIpResolverMock.VerifyAll();
         _ipWhitelistingSettingsProviderMock.VerifyAll();
         _testServer.Dispose();
      }

      [TestMethod]
      public async Task IpWhitelistingMiddleware_Invoke_IpIsWhitelisted_ShouldContinue()
      {
         // arrange
         var whitelisting = new[]
         {
            new IpWhitelisting
            {
               Path = "/test123",
               AllowedIps = new[]
               {
                  "1.2.3.4"
               }
            }
         };

         _ipWhitelistingSettingsProviderMock
            .Setup(m => m.GetIpWhitelistings())
            .Returns(whitelisting);

         _clientIpResolverMock
            .Setup(m => m.GetClientIp())
            .Returns("1.2.3.4");

         using (var response = await _httpClient.GetAsync("/test123"))
         {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            string content = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("OK", content);
         }
      }

      [TestMethod]
      public async Task IpWhitelistingMiddleware_Invoke_IpIsWhitelisted_ShouldContinue_CaseInsensitive()
      {
         // arrange
         var whitelisting = new[]
         {
            new IpWhitelisting
            {
               Path = "/TEST123",
               AllowedIps = new[]
               {
                  "1.2.3.4"
               }
            }
         };

         _ipWhitelistingSettingsProviderMock
            .Setup(m => m.GetIpWhitelistings())
            .Returns(whitelisting);

         _clientIpResolverMock
            .Setup(m => m.GetClientIp())
            .Returns("1.2.3.4");

         using (var response = await _httpClient.GetAsync("/test123"))
         {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            string content = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("OK", content);
         }
      }

      [TestMethod]
      public async Task IpWhitelistingMiddleware_Invoke_IpRangeIsWhitelisted_ShouldContinue()
      {
         // arrange
         var whitelisting = new[]
         {
            new IpWhitelisting
            {
               Path = "/test123",
               AllowedIps = new[]
               {
                  "192.168.0.0/29"
               }
            }
         };

         _ipWhitelistingSettingsProviderMock
            .Setup(m => m.GetIpWhitelistings())
            .Returns(whitelisting);

         _clientIpResolverMock
            .Setup(m => m.GetClientIp())
            .Returns("192.168.0.6");

         using (var response = await _httpClient.GetAsync("/test123"))
         {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            string content = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("OK", content);
         }
      }

      [TestMethod]
      public async Task IpWhitelistingMiddleware_Invoke_IpIsNotWhitelisted_ShouldReturn401()
      {
         // arrange
         var whitelisting = new[]
         {
            new IpWhitelisting
            {
               Path = "/test123",
               AllowedIps = new[]
               {
                  "1.2.3.5"
               }
            }
         };

         _ipWhitelistingSettingsProviderMock
            .Setup(m => m.GetIpWhitelistings())
            .Returns(whitelisting);

         _clientIpResolverMock
            .Setup(m => m.GetClientIp())
            .Returns("1.2.3.4");

         using (var response = await _httpClient.GetAsync("/test123"))
         {
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
         }
      }

      [TestMethod]
      public async Task IpWhitelistingMiddleware_Invoke_IpIsNotInWhitelistedRange_ShouldReturn401()
      {
         // arrange
         var whitelisting = new[]
         {
            new IpWhitelisting
            {
               Path = "/test123",
               AllowedIps = new[]
               {
                  "192.168.0.0/29"
               }
            }
         };

         _ipWhitelistingSettingsProviderMock
            .Setup(m => m.GetIpWhitelistings())
            .Returns(whitelisting);

         _clientIpResolverMock
            .Setup(m => m.GetClientIp())
            .Returns("192.168.0.8");

         using (var response = await _httpClient.GetAsync("/test123"))
         {
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
         }
      }

      private void ConfigureServices(IServiceCollection services)
      {
         services
            .AddHttpContextAccessor()
            .AddSingleton(_ipWhitelistingSettingsProviderMock.Object)
            .AddSingleton(_clientIpResolverMock.Object)
            .AddCustomMvcServices();
      }

      private void Configure(IApplicationBuilder app)
      {
         app.UseIpWhitelisting();
         app.Run(async context =>
         {
            await context.Response.WriteAsync("OK");
         });
      }
   }
}
