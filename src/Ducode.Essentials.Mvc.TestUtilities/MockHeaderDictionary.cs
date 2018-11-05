using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Ducode.Essentials.Mvc.TestUtilities
{
   internal class MockHeaderDictionary : Dictionary<string, StringValues>, IHeaderDictionary
   {
      public long? ContentLength { get; set; }
   }
}
