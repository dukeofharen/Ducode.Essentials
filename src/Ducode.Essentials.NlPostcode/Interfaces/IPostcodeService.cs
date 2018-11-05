using System.Threading.Tasks;
using Ducode.Essentials.NlPostcode.Models;

namespace Ducode.Essentials.NlPostcode.Interfaces
{
   /// <summary>
   /// Describes a class that is used to query for Dutch postcodes.
   /// </summary>
   public interface IPostcodeService
   {
      /// <summary>
      /// Searches for a Dutch post code asynchronous.
      /// </summary>
      /// <param name="postcode">The postcode.</param>
      /// <returns>A <see cref="PostcodeWrapper"/> containing information about the postcode.</returns>
      Task<PostcodeWrapper> GetPostcodeAsync(string postcode);
   }
}
