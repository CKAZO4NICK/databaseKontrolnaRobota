

using Microsoft.EntityFrameworkCore;

namespace KontrolnaRobota.Database.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> Table { get; }
        void Create(TEntity item);
        TEntity FindById(int id);
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        void Remove(TEntity item);
        void Update(TEntity item);
        void SaveChanges();
    }
}
