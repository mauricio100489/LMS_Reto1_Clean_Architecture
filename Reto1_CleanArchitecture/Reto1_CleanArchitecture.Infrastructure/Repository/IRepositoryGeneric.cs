using System.Linq.Expressions;

namespace Reto1_CleanArchitecture.Infrastructure.Repository
{
    public interface IRepositoryGeneric<TEntity> where TEntity : class
    {
        IQueryable<TEntity> All();

        TEntity Create();

        TEntity Create(TEntity entity);

        Task CreateRangeAsync(List<TEntity> listEntity);

        TEntity Delete(TEntity entity);

        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);

        TEntity? Find(params object[] keys);

        ValueTask<TEntity?> FindAsync(params object[] keys);

        ValueTask<TEntity?> FindAsync(CancellationToken token, params object[] keys);

        TEntity First(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        int SaveChanges();

        Task<int> SaveChangesAsync();

        IQueryable<TResult> Transform<TResult>(Expression<Func<TEntity, TResult>> predicate);

        TEntity Update(TEntity entity);

        TEntity Update(TEntity oldEntity, TEntity newEntity);

        Task<List<TEntity>> UpdateRangeAsync(List<TEntity> listEntity);

        void SetOriginalValue<TValue>(TEntity entity, string propertyName, TValue value);
    }
}
