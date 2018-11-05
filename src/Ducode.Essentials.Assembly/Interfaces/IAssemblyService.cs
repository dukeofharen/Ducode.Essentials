namespace Ducode.Essentials.Assembly.Interfaces
{
   /// <summary>
   /// Describes a class that is used to perform several assembly related tasks.
   /// </summary>
   public interface IAssemblyService
   {
      /// <summary>
      /// Returns the root path of the entry assembly.
      /// </summary>
      /// <returns>The root path of the entry assembly.</returns>
      string GetEntryAssemblyRootPath();

      /// <summary>
      /// Returns the root path of the executing assembly.
      /// </summary>
      /// <returns>The root path of the executing assembly.</returns>
      string GetExecutingAssemblyRootPath();

      /// <summary>
      /// Gets the assembly version of the running application.
      /// </summary>
      /// <returns>The assembly version.</returns>
      string GetAssemblyVersion();
   }
}
