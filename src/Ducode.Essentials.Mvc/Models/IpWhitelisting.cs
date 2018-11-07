using System.Collections.Generic;

namespace Ducode.Essentials.Mvc.Models
{
   /// <summary>
   /// A model for storing IP whitelisting settings.
   /// </summary>
   public class IpWhitelisting
   {
      /// <summary>
      /// Gets or sets the path.
      /// </summary>
      /// <value>
      /// The path.
      /// </value>
      public string Path { get; set; }

      /// <summary>
      /// Gets or sets the allowed IPs.
      /// </summary>
      /// <value>
      /// The allowed ips.
      /// </value>
      public IEnumerable<string> AllowedIps { get; set; }
   }
}
