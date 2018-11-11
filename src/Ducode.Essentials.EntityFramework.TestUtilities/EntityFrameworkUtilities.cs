using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Ducode.Essentials.EntityFramework.TestUtilities
{
   internal static class EntityFrameworkUtilities
   {
      public static DbSet<TEntity> GetDbSet<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
      {
         var query = entities.AsQueryable();

         var mockSet = new Mock<DbSet<TEntity>>();

         mockSet.As<IAsyncEnumerable<TEntity>>()
           .Setup(m => m.GetEnumerator())
           .Returns(new TestAsyncEnumerator<TEntity>(query.GetEnumerator()));

         mockSet.As<IQueryable<TEntity>>()
            .Setup(m => m.Provider)
            .Returns(new TestAsyncQueryProvider<TEntity>(query.Provider));

         mockSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(query.Expression);
         mockSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(query.ElementType);
         mockSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(() => query.GetEnumerator());

         return mockSet.Object;
      }
   }
}
