namespace Ducode.Essentials.NlPostcode
{
   /// <summary>
   /// A static class containing several options for the NlPostcode library.
   /// </summary>
   public static class NlPostcodeConstants
   {
      /// <summary>
      /// Gets or sets the pdok suggest URL.
      /// </summary>
      /// <value>
      /// The pdok suggest URL.
      /// </value>
      public static string PdokSuggestUrl { get; set; } = "https://geodata.nationaalgeoregister.nl/locatieserver/v3/suggest?q={0}";
   }
}
