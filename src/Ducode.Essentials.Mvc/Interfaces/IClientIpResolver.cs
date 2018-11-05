namespace Ducode.Essentials.Mvc.Interfaces
{
   /// <summary>
   /// Describes a class used for retrieving the IP address from a client.
   /// </summary>
   public interface IClientIpResolver
   {
      /// <summary>
      /// Gets the client IP address.
      /// </summary>
      /// <returns>The client IP address.</returns>
      string GetClientIp();
   }
}
