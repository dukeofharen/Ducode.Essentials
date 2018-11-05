using Ducode.Essentials.Assembly.Interfaces;

namespace Ducode.Essentials.Assembly
{
   /// <summary>
   /// A class that is used to perform several assembly related tasks.
   /// </summary>
   /// <seealso cref="Ducode.Essentials.Assembly.Interfaces.IAssemblyService" />
   public class AssemblyService : IAssemblyService
   {
      /// <summary>
      /// Gets the assembly version of the running application.
      /// </summary>
      /// <returns>
      /// The assembly version.
      /// </returns>
      public string GetAssemblyVersion()
      {
         return AssemblyHelper.GetAssemblyVersion();
      }

      /// <summary>
      /// Returns the root path of the entry assembly.
      /// </summary>
      /// <returns>
      /// The root path of the entry assembly.
      /// </returns>
      public string GetEntryAssemblyRootPath()
      {
         return AssemblyHelper.GetEntryAssemblyRootPath();
      }

      /// <summary>
      /// Returns the root path of the executing assembly.
      /// </summary>
      /// <returns>
      /// The root path of the executing assembly.
      /// </returns>
      public string GetExecutingAssemblyRootPath()
      {
         return AssemblyHelper.GetExecutingAssemblyRootPath();
      }
   }
}
