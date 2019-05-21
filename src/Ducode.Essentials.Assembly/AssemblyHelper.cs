using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ducode.Essentials.Assembly
{
   /// <summary>
   /// A static class containing several assembly related helper methods.
   /// </summary>
   public static class AssemblyHelper
   {
        /// <summary>
        /// Gets all implementations of a specific interface.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="assemblyFilter">Filters assembly on a given substring. If not set, check all assemblies.</param>
        /// <returns>A list with all implementation types.</returns>
        public static IEnumerable<Type> GetImplementations<TInterface>(string assemblyFilter = "")
        {
            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies();
            if (!string.IsNullOrWhiteSpace(assemblyFilter))
            {
                assemblies = assemblies.Where(a => a.FullName.Contains(assemblyFilter)).ToArray();
            }

            var types = assemblies
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(TInterface).IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
                .ToArray();

            return types;
        }

        /// <summary>
        /// Returns the root path of the entry assembly.
        /// </summary>
        /// <returns>The root path of the entry assembly.</returns>
        public static string GetEntryAssemblyRootPath()
      {
         var assembly = System.Reflection.Assembly.GetEntryAssembly();
         string path = assembly.Location;
         return Path.GetDirectoryName(path);
      }

      /// <summary>
      /// Returns the root path of the executing assembly.
      /// </summary>
      /// <returns>The root path of the executing assembly.</returns>
      public static string GetExecutingAssemblyRootPath()
      {
         var assembly = System.Reflection.Assembly.GetExecutingAssembly();
         string path = assembly.Location;
         return Path.GetDirectoryName(path);
      }

      /// <summary>
      /// Returns the root path of the calling assembly.
      /// </summary>
      /// <returns>The root path of the calling assembly.</returns>
      public static string GetCallingAssemblyRootPath()
      {
         var assembly = System.Reflection.Assembly.GetCallingAssembly();
         string path = assembly.Location;
         return Path.GetDirectoryName(path);
      }

      /// <summary>
      /// Gets the assembly version of the running application.
      /// </summary>
      /// <returns>The assembly version.</returns>
      public static string GetAssemblyVersion()
      {
         return System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
      }
   }
}
