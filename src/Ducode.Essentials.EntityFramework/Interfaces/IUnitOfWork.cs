using System;
using System.Threading.Tasks;

namespace Ducode.Essentials.EntityFramework.Interfaces
{
   /// <summary>
   /// Describes a unit of work, which is a wrapper around the database context.
   /// </summary>
   public interface IUnitOfWork : IDisposable
   {
      /// <summary>
      /// Gets a repository of a specific entity.
      /// </summary>
      /// <typeparam name="TEntity">The type of the entity.</typeparam>
      /// <returns>A repository of a specific entity.</returns>
      IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

      /// <summary>
      /// Saves the changes.
      /// </summary>
      /// <returns>The number of updates written to the database.</returns>
      int SaveChanges();

      /// <summary>
      /// Saves the changes asynchronous.
      /// </summary>
      /// <returns>The number of updates written to the database.</returns>
      Task<int> SaveChangesAsync();
   }
}
