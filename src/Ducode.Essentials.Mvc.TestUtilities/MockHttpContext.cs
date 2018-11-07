using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace Ducode.Essentials.Mvc.TestUtilities
{
   /// <summary>
   /// A mock HttpContext used for unit testing.
   /// </summary>
   /// <seealso cref="Microsoft.AspNetCore.Http.HttpContext" />
   public class MockHttpContext : HttpContext
   {
      private string _actualRedirectUrl;
      private int _statusCode;

      /// <summary>
      /// Initializes a new instance of the <see cref="MockHttpContext"/> class.
      /// </summary>
      public MockHttpContext()
      {
         ConnectionInfoMock = new Mock<ConnectionInfo>();
         HttpResponseMock = new Mock<HttpResponse>();
         HttpRequestMock = new Mock<HttpRequest>();
         ServiceProviderMock = new Mock<IServiceProvider>();
         Items = new Dictionary<object, object>();
         Session = new MockSession();

         HttpResponseMock
            .SetupSet(m => m.StatusCode = It.IsAny<int>())
            .Callback<int>(code => _statusCode = code);

         ServiceProviderMock
            .Setup(m => m.GetService(typeof(ITempDataDictionaryFactory)))
            .Returns(new MockTempDataDictionaryFactory());

         ServiceProviderMock
            .Setup(m => m.GetService(typeof(IUrlHelperFactory)))
            .Returns(new Mock<IUrlHelperFactory>().Object);

         HttpRequestMock
            .Setup(m => m.Headers)
            .Returns(new MockHeaderDictionary());

         HttpResponseMock
            .Setup(m => m.Headers)
            .Returns(new MockHeaderDictionary());

         HttpResponseMock
            .Setup(m => m.Redirect(It.IsAny<string>()))
            .Callback<string>(u => _actualRedirectUrl = u);
      }

      /// <summary>
      /// Gets the connection information mock.
      /// </summary>
      /// <value>
      /// The connection information mock.
      /// </value>
      public Mock<ConnectionInfo> ConnectionInfoMock { get; }

      /// <summary>
      /// Gets the HTTP request mock.
      /// </summary>
      /// <value>
      /// The HTTP request mock.
      /// </value>
      public Mock<HttpRequest> HttpRequestMock { get; }

      /// <summary>
      /// Gets the HTTP response mock.
      /// </summary>
      /// <value>
      /// The HTTP response mock.
      /// </value>
      public Mock<HttpResponse> HttpResponseMock { get; }

      /// <summary>
      /// Gets the service provider mock.
      /// </summary>
      /// <value>
      /// The service provider mock.
      /// </value>
      public Mock<IServiceProvider> ServiceProviderMock { get; }

      /// <summary>
      /// Gets the response status code.
      /// </summary>
      /// <returns>The response status code.</returns>
      public int GetStatusCode() => _statusCode;

      /// <summary>
      /// Gets the response redirect URL.
      /// </summary>
      /// <returns>The response redirect URL.</returns>
      public string GetRedirectUrl() => _actualRedirectUrl;

      /// <summary>
      /// Sets the request IP address.
      /// </summary>
      /// <param name="ip">The ip.</param>
      public void SetIp(byte[] ip)
      {
         ConnectionInfoMock
            .Setup(m => m.RemoteIpAddress)
            .Returns(new IPAddress(ip));
      }

      /// <summary>
      /// Sets the request IP address.
      /// </summary>
      /// <param name="ip">The ip.</param>
      public void SetIp(string ip)
      {
         ConnectionInfoMock
            .Setup(m => m.RemoteIpAddress)
            .Returns(IPAddress.Parse(ip));
      }

      /// <summary>
      /// Sets the HTTP host.
      /// </summary>
      /// <param name="host">The host.</param>
      public void SetHost(string host)
      {
         HttpRequestMock
            .Setup(m => m.Host)
            .Returns(new HostString(host));
      }

      /// <summary>
      /// Initializes a new user with identifier claim.
      /// </summary>
      /// <param name="id">The identifier.</param>
      public void InitializeUserWithId(long id)
      {
         InitializeUserWithId(id.ToString());
      }

      /// <summary>
      /// Initializes a new user with identifier claim.
      /// </summary>
      /// <param name="id">The identifier.</param>
      public void InitializeUserWithId(string id)
      {
         User = new ClaimsPrincipal(new[]
         {
            new ClaimsIdentity(new[]
            {
               new Claim(ClaimTypes.NameIdentifier, id)
            })
         });
      }

      /// <summary>
      /// Sets the request user.
      /// </summary>
      /// <param name="user">The user.</param>
      public void SetUser(ClaimsPrincipal user)
      {
         User = user;
      }

      /// <summary>
      /// Sets the request HTTP method.
      /// </summary>
      /// <param name="method">The method.</param>
      public void SetRequestMethod(string method)
      {
         HttpRequestMock
            .Setup(m => m.Method)
            .Returns(method);
      }

      /// <summary>
      /// Sets the HTTP query string.
      /// </summary>
      /// <param name="queryString">The query string.</param>
      public void SetQueryString(string queryString)
      {
         HttpRequestMock
            .Setup(m => m.QueryString)
            .Returns(new QueryString(queryString));
      }

      /// <summary>
      /// Sets the request path.
      /// </summary>
      /// <param name="path">The path.</param>
      public void SetRequestPath(string path)
      {
         var pathString = new PathString(path);
         HttpRequestMock
            .Setup(m => m.Path)
            .Returns(pathString);
      }

      /// <summary>
      /// Sets the request to HTTP(s).
      /// </summary>
      /// <param name="isHttps">if set to <c>true</c> [is HTTPS].</param>
      public void SetHttps(bool isHttps)
      {
         HttpRequestMock
            .Setup(m => m.IsHttps)
            .Returns(isHttps);
      }

      /// <summary>
      /// Aborts the connection underlying this request.
      /// </summary>
      /// <exception cref="System.NotImplementedException"></exception>
      public override void Abort()
      {
         throw new NotImplementedException();
      }

      /// <summary>
      /// Verifies all mock setups.
      /// </summary>
      public void VerifyAll()
      {
         ConnectionInfoMock.VerifyAll();
         HttpRequestMock.VerifyAll();
         HttpResponseMock.VerifyAll();
      }

      /// <summary>
      /// Gets the collection of HTTP features provided by the server and middleware available on this request.
      /// </summary>
      public override IFeatureCollection Features { get; }

      /// <summary>
      /// Gets the <see cref="T:Microsoft.AspNetCore.Http.HttpRequest" /> object for this request.
      /// </summary>
      public override HttpRequest Request => HttpRequestMock.Object;

      /// <summary>
      /// Gets the <see cref="T:Microsoft.AspNetCore.Http.HttpResponse" /> object for this request.
      /// </summary>
      public override HttpResponse Response => HttpResponseMock.Object;

      /// <summary>
      /// Gets information about the underlying connection for this request.
      /// </summary>
      public override ConnectionInfo Connection => ConnectionInfoMock.Object;

      /// <summary>
      /// Gets an object that manages the establishment of WebSocket connections for this request.
      /// </summary>
      public override WebSocketManager WebSockets { get; }

      /// <summary>
      /// This is obsolete and will be removed in a future version.
      /// The recommended alternative is to use Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.
      /// See https://go.microsoft.com/fwlink/?linkid=845470.
      /// </summary>
      [Obsolete("")]
      public override Microsoft.AspNetCore.Http.Authentication.AuthenticationManager Authentication { get; }

      /// <summary>
      /// Gets or sets the user for this request.
      /// </summary>
      public override ClaimsPrincipal User { get; set; }

      /// <summary>
      /// Gets or sets a key/value collection that can be used to share data within the scope of this request.
      /// </summary>
      public override IDictionary<object, object> Items { get; set; }

      /// <summary>
      /// Gets or sets the <see cref="T:System.IServiceProvider" /> that provides access to the request's service container.
      /// </summary>
      /// <exception cref="System.NotImplementedException"></exception>
      public override IServiceProvider RequestServices
      {
         get => ServiceProviderMock.Object;
         set => throw new NotImplementedException();
      }

      /// <summary>
      /// Notifies when the connection underlying this request is aborted and thus request operations should be
      /// cancelled.
      /// </summary>
      public override CancellationToken RequestAborted { get; set; }

      /// <summary>
      /// Gets or sets a unique identifier to represent this request in trace logs.
      /// </summary>
      public override string TraceIdentifier { get; set; }

      /// <summary>
      /// Gets or sets the object used to manage user session data for this request.
      /// </summary>
      public override ISession Session { get; set; }
   }
}
