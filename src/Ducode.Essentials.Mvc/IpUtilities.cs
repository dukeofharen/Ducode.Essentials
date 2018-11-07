namespace Ducode.Essentials.Mvc
{
   /// <summary>
   /// A static class containing several IP methods.
   /// </summary>
   public static class IpUtilities
   {
      /// <summary>
      /// Normalizes an IP address.
      /// </summary>
      /// <param name="ip">The ip.</param>
      /// <returns>The normalized IP address.</returns>
      public static string NormalizeIp(string ip) => ip == "::1" ? "127.0.0.1" : ip;
   }
}
