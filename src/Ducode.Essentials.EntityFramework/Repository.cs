using System;
using System.Collections.Generic;
using System.Linq;
using Ducode.Essentials.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ducode.Essentials.EntityFramework
{
   /// <summary>
   /// A class that is used to represent the repository of a single entity.
   /// </summary>
   /// <typeparam name="TEntity">The type of the entity.</typeparam>
   public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
   {
      private readonly DbContext _context;

      /// <summary>
      /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
      /// </summary>
      /// <param name="context">The context.</param>
      public Repository(DbContext context)
      {
         _context = context;
      }

      /// <summary>
      /// Adds the specified entity.
      /// </summary>
      /// <param name="entity">The entity.</param>
      /// <returns>
      /// The added entity.
      /// </returns>
      public TEntity Add(TEntity entity)
      {
         var set = GetSet();
         var result = set.Add(entity);
         return result.Entity;
      }

      /// <summary>
      /// Retrieves the collection of entities.
      /// </summary>
      /// <returns>
      /// Collection of entities.
      /// </returns>
      public IQueryable<TEntity> All()
      {
         return GetSet();
      }

      /// <summary>
      /// Deletes the specified entity.
      /// </summary>
      /// <param name="entity">The entity.</param>
      public void Delete(TEntity entity)
      {
         var set = GetSet();
         set.Remove(entity);
      }

      /// <summary>
      /// Deletes the range of entities.
      /// </summary>
      /// <param name="entities">The entities.</param>
      public void DeleteRange(IEnumerable<TEntity> entities)
      {
         var set = GetSet();
         set.RemoveRange(entities);
      }

      private DbSet<TEntity> GetSet()
      {
         var set = _context.Set<TEntity>();
         if (set == null)
         {
            throw new InvalidOperationException(string.Format("'{0}' isn't an entity type!", typeof(TEntity).Name));
         }

         return set;
      }
   }
}
