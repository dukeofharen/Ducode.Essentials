using Newtonsoft.Json;

namespace Ducode.Essentials.NlPostcode.Models
{
   /// <summary>
   /// A class containing whether the request was successful and the containing postcode result.
   /// </summary>
   public class PostcodeWrapper
   {
      /// <summary>
      /// Gets or sets a value indicating whether this <see cref="PostcodeWrapper"/> is success.
      /// </summary>
      /// <value>
      ///   <c>true</c> if success; otherwise, <c>false</c>.
      /// </value>
      [JsonProperty("success")]
      public bool Success { get; set; }

      /// <summary>
      /// Gets or sets the resource.
      /// </summary>
      /// <value>
      /// The resource.
      /// </value>
      [JsonProperty("resource")]
      public PostcodeResult Resource { get; set; }
   }
}
