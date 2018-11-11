using System;
using System.Threading.Tasks;
using Ducode.Essentials.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ducode.Essentials.EntityFramework
{
   /// <summary>
   /// A unit of work, which is a wrapper around the database context.
   /// </summary>
   public class UnitOfWork : IUnitOfWork
   {
      private DbContext _context;

      /// <summary>
      /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
      /// </summary>
      /// <param name="context">The context.</param>
      public UnitOfWork(DbContext context)
      {
         _context = context;
      }

      /// <summary>
      /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
      /// </summary>
      public void Dispose()
      {
         if (_context != null)
         {
            Dispose(true);
            GC.SuppressFinalize(this);
         }
      }

      /// <summary>
      /// Releases unmanaged and - optionally - managed resources.
      /// </summary>
      /// <param name="cleanUpAllResources"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
      protected virtual void Dispose(bool cleanUpAllResources)
      {
         _context.Dispose();
         _context = null;
      }

      /// <summary>
      /// Saves the changes asynchronous.
      /// </summary>
      /// <returns>
      /// The number of updates written to the database.
      /// </returns>
      public Task<int> SaveChangesAsync()
      {
         return _context.SaveChangesAsync();
      }

      /// <summary>
      /// Saves the changes.
      /// </summary>
      /// <returns>
      /// The number of updates written to the database.
      /// </returns>
      public int SaveChanges()
      {
         return _context.SaveChanges();
      }

      /// <summary>
      /// Gets a repository of a specific entity.
      /// </summary>
      /// <typeparam name="TEntity">The type of the entity.</typeparam>
      /// <returns>
      /// A repository of a specific entity.
      /// </returns>
      public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
      {
         var repo = new Repository<TEntity>(_context) as IRepository<TEntity>;
         return repo;
      }
   }
}
