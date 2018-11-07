using System.Collections.Generic;
using Ducode.Essentials.Mvc.Models;

namespace Ducode.Essentials.Mvc.Interfaces
{
   /// <summary>
   /// Describes a class that is used for retrieving IP whitelisting settings.
   /// </summary>
   public interface IIpWhitelistingSettingsProvider
   {
      /// <summary>
      /// Gets the IP whitelistings.
      /// </summary>
      /// <returns>A list of <see cref="IpWhitelisting"/>.</returns>
      IEnumerable<IpWhitelisting> GetIpWhitelistings();
   }
}
