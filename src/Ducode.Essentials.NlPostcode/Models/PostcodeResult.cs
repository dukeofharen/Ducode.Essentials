using Newtonsoft.Json;

namespace Ducode.Essentials.NlPostcode.Models
{
   /// <summary>
   /// A model containing the postcode information.
   /// </summary>
   public class PostcodeResult
   {
      /// <summary>
      /// Gets or sets the street.
      /// </summary>
      /// <value>
      /// The street.
      /// </value>
      [JsonProperty("street")]
      public string Street { get; set; }

      /// <summary>
      /// Gets or sets the postcode.
      /// </summary>
      /// <value>
      /// The postcode.
      /// </value>
      [JsonProperty("postcode")]
      public string Postcode { get; set; }

      /// <summary>
      /// Gets or sets the town.
      /// </summary>
      /// <value>
      /// The town.
      /// </value>
      [JsonProperty("town")]
      public string Town { get; set; }
   }
}
