using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ducode.Essentials.NlPostcode.Interfaces;
using Ducode.Essentials.NlPostcode.Models;
using Ducode.Essentials.Web;
using Ducode.Essentials.Web.Interfaces;
using Newtonsoft.Json.Linq;

namespace Ducode.Essentials.NlPostcode
{
   /// <summary>
   /// A class that is used to query for Dutch postcodes.
   /// </summary>
   public class PostcodeService : IPostcodeService
   {
      private readonly IWebService _webService;

      /// <summary>
      /// Initializes a new instance of the <see cref="PostcodeService"/> class.
      /// </summary>
      public PostcodeService() : this(new WebService())
      {
      }

      internal PostcodeService(IWebService webService)
      {
         _webService = webService;
      }

      /// <summary>
      /// Searches for a Dutch post code asynchronous.
      /// </summary>
      /// <param name="postcode">The postcode.</param>
      /// <returns>
      /// A <see cref="T:Ducode.Essentials.NlPostcode.Models.PostcodeWrapper" /> containing information about the postcode.
      /// </returns>
      /// <exception cref="ArgumentNullException">postcode</exception>
      /// <exception cref="ArgumentException">Postcode not 6 characters long.</exception>
      /// <exception cref="InvalidOperationException">
      /// Invalid response object
      /// or
      /// weergavenaam is empty
      /// or
      /// Length of matches not 4
      /// </exception>
      public async Task<PostcodeWrapper> GetPostcodeAsync(string postcode)
      {
         if (string.IsNullOrEmpty(postcode))
         {
            throw new ArgumentNullException(nameof(postcode));
         }

         postcode = postcode.Replace(" ", string.Empty);
         if (postcode.Length != 6)
         {
            throw new ArgumentException("Postcode not 6 characters long.");
         }

         postcode = postcode.ToUpper();
         string url = string.Format(NlPostcodeConstants.PdokSuggestUrl, postcode);

         var response = await _webService.GetAsync(url);
         string content = await response.Content.ReadAsStringAsync();

         var token = JToken.Parse(content);
         if (token["response"] == null)
         {
            throw new InvalidOperationException("Invalid response object");
         }

         var internalResponse = token["response"];
         if (internalResponse["docs"] == null)
         {
            return new PostcodeWrapper
            {
               Success = false
            };
         }

         var docs = internalResponse["docs"];
         if (docs.All(d => d["type"]?.ToString() != "postcode"))
         {
            return new PostcodeWrapper
            {
               Success = false
            };
         }

         var postcodeElement = docs.First(pe => pe["type"]?.ToString() == "postcode");
         string displayName = postcodeElement["weergavenaam"].ToString();
         if (string.IsNullOrWhiteSpace(displayName))
         {
            throw new InvalidOperationException("weergavenaam is empty");
         }

         var regex = new Regex(@"(.*),\s([1-9][0-9]{3}[\s]?[A-Za-z]{2})\s(.*)");
         var match = regex.Match(displayName);
         if (match.Groups.Count != 4)
         {
            throw new InvalidOperationException("Length of matches not 4");
         }

         var wrapper = new PostcodeWrapper
         {
            Success = true,
            Resource = new PostcodeResult
            {
               Street = match.Groups[1].Value,
               Postcode = match.Groups[2].Value,
               Town = match.Groups[3].Value
            }
         };

         return wrapper;
      }
   }
}
