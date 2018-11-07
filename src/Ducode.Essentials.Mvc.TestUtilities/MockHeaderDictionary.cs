using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Ducode.Essentials.Mvc.TestUtilities
{
   /// <summary>
   /// A dictionary used for mocking purposes.
   /// </summary>
   /// <seealso cref="Microsoft.AspNetCore.Http.IHeaderDictionary" />
   public class MockHeaderDictionary : Dictionary<string, StringValues>, IHeaderDictionary
   {
      /// <summary>
      /// Strongly typed access to the Content-Length header. Implementations must keep this in sync with the string representation.
      /// </summary>
      public long? ContentLength { get; set; }
   }
}
