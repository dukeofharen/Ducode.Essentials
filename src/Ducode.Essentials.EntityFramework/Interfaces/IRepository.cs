using System.Collections.Generic;
using System.Linq;

namespace Ducode.Essentials.EntityFramework.Interfaces
{
   /// <summary>
   /// Describes a class that is used to represent the repository of a single entity.
   /// </summary>
   /// <typeparam name="TEntity">The type of the entity.</typeparam>
   public interface IRepository<TEntity> where TEntity : class
   {
      /// <summary>
      /// Retrieves the collection of entities.
      /// </summary>
      /// <returns>Collection of entities.</returns>
      IQueryable<TEntity> All();

      /// <summary>
      /// Adds the specified entity.
      /// </summary>
      /// <param name="entity">The entity.</param>
      /// <returns>The added entity.</returns>
      TEntity Add(TEntity entity);

      /// <summary>
      /// Deletes the specified entity.
      /// </summary>
      /// <param name="entity">The entity.</param>
      void Delete(TEntity entity);

      /// <summary>
      /// Deletes the range of entities.
      /// </summary>
      /// <param name="entities">The entities.</param>
      void DeleteRange(IEnumerable<TEntity> entities);
   }
}
