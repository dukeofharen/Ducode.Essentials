namespace Ducode.Essentials.EntityFramework.Interfaces
{
   /// <summary>
   /// Describes a class that is used to create instances of <see cref="IUnitOfWork"/>.
   /// </summary>
   public interface IUnitOfWorkFactory
   {
      /// <summary>
      /// Creates a new <see cref="IUnitOfWork"/>.
      /// </summary>
      /// <returns>A new <see cref="IUnitOfWork"/>.</returns>
      IUnitOfWork Create();
   }
}
