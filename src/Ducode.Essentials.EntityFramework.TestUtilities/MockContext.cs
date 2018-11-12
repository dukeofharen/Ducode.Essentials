using System;
using System.Collections.Generic;
using System.Linq;
using Ducode.Essentials.EntityFramework.Interfaces;
using Moq;

namespace Ducode.Essentials.EntityFramework.TestUtilities
{
   /// <summary>
   /// A mock context around the unit of work pattern for unit test scenarios.
   /// </summary>
   /// <typeparam name="TUnitOfWorkFactory">The type of the unit of work factory.</typeparam>
   public class MockContext<TUnitOfWorkFactory> where TUnitOfWorkFactory : class, IUnitOfWorkFactory
   {
      private Random _random;
      private int _next;

      /// <summary>
      /// Initializes a new instance of the <see cref="MockContext{TUnitOfWorkFactory}"/> class.
      /// </summary>
      public MockContext()
      {
         Entities = new List<object>();
         _random = new Random();

         var unitOfWorkFactoryMock = new Mock<TUnitOfWorkFactory>(MockBehavior.Strict);
         UnitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);

         unitOfWorkFactoryMock.
            Setup(m => m.Create())
            .Returns(UnitOfWorkMock.Object);
         UnitOfWorkMock.Setup(m => m.Dispose());
         UnitOfWorkMock.Setup(m => m.SaveChangesAsync()).ReturnsAsync(Random());
         UnitOfWorkMock.Setup(m => m.SaveChanges()).Returns(Random());

         UnitOfWorkFactory = unitOfWorkFactoryMock.Object;
      }

      /// <summary>
      /// Gets the entities.
      /// </summary>
      /// <value>
      /// The entities.
      /// </value>
      public List<object> Entities { get; }

      /// <summary>
      /// Gets or sets the before add function, useful for capturing when an entity is added.
      /// </summary>
      /// <value>
      /// The before add function.
      /// </value>
      public Action<object> BeforeAdd { get; set; }

      /// <summary>
      /// Gets the unit of work factory.
      /// </summary>
      /// <value>
      /// The unit of work factory.
      /// </value>
      public TUnitOfWorkFactory UnitOfWorkFactory { get; }

      /// <summary>
      /// Gets the unit of work mock.
      /// </summary>
      /// <value>
      /// The unit of work mock.
      /// </value>
      public Mock<IUnitOfWork> UnitOfWorkMock { get; }

      /// <summary>
      /// Gets the list of entities.
      /// </summary>
      /// <typeparam name="TEntity">The type of the entity.</typeparam>
      /// <returns>The list of entities.</returns>
      public IList<TEntity> GetList<TEntity>()
      {
         return Entities
                  .Where(e => e is TEntity)
                  .Cast<TEntity>()
                  .ToList();
      }

      /// <summary>
      /// Gets a random number.
      /// </summary>
      /// <returns>A random number.</returns>
      public int Random()
      {
         return _random.Next();
      }

      /// <summary>
      /// Gets a random number.
      /// </summary>
      /// <param name="max">The maximum.</param>
      /// <returns>
      /// A random number.
      /// </returns>
      public int Random(int max)
      {
         return _random.Next(0, max);
      }

      /// <summary>
      /// Gets a random number.
      /// </summary>
      /// <param name="min">The minimum.</param>
      /// <param name="max">The maximum.</param>
      /// <returns>
      /// A random number.
      /// </returns>
      public int Random(int min, int max)
      {
         return _random.Next(min, max);
      }

      /// <summary>
      /// Gets the next number.
      /// </summary>
      /// <returns>The next number.</returns>
      public int Next()
      {
         _next++;
         return _next;
      }

      /// <summary>
      /// Adds the specified entity.
      /// </summary>
      /// <typeparam name="TEntity">The type of the entity.</typeparam>
      /// <param name="entity">The entity.</param>
      /// <returns>The added entity.</returns>
      public TEntity Add<TEntity>(TEntity entity)
      {
         Entities.Add(entity);
         return entity;
      }

      /// <summary>
      /// Clears all entities.
      /// </summary>
      public void Clear()
      {
         Entities.Clear();
      }

      /// <summary>
      /// Initializes the repository of a specific entity.
      /// </summary>
      /// <typeparam name="TEntity">The type of the entity.</typeparam>
      public void SetupRepository<TEntity>() where TEntity : class
      {
         UnitOfWorkMock
            .Setup(m => m.GetRepository<TEntity>())
            .Returns(CreateRepository<TEntity>());
      }

      private IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class
      {
         var repoMock = new Mock<IRepository<TEntity>>(MockBehavior.Strict);
         repoMock.Setup(m => m.All()).Returns(EntityFrameworkUtilities.GetDbSet(Entities.Where(e => e is TEntity).Cast<TEntity>().AsQueryable()));
         repoMock.Setup(m =>
            m.Add(It.IsAny<TEntity>()))
            .Callback<TEntity>(e =>
            {
               BeforeAdd?.Invoke(e);
               Entities.Add(e);
            })
            .Returns<TEntity>(e => e);
         repoMock.Setup(m => m.Delete(It.IsAny<TEntity>())).Callback<TEntity>(e =>
         {
            Entities.Remove(e);
         });
         return repoMock.Object;
      }
   }
}
