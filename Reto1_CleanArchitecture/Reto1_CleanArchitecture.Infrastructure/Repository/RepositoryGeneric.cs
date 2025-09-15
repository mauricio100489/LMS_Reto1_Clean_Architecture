using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Reto1_CleanArchitecture.Infrastructure.Repository
{
    public class RepositoryGeneric<TEntity, TContext> : IRepositoryGeneric<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        public RepositoryGeneric(TContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Gets or sets the context used in this repository.
        /// </summary>
        public virtual TContext Context { get; protected set; }

        /// <summary>
        /// When is override in a deriver type, returns all the elements in the entity set.
        /// </summary>
        /// <returns>
        /// An <see cref="IQueryable"/> for all the elements in the entity set.
        /// </returns>
        public virtual IQueryable<TEntity> All()
        {
            return this.Context.Set<TEntity>();
        }

        /// <summary>
        /// Filters an entity set of values based on a predicate.
        /// </summary>
        /// <param name="predicate">
        /// An expression to test each element for a condition.
        /// </param>
        /// <returns>
        /// An <see cref="IQueryable"/> that contains elements from the input sequence that satisfy the condition specified by predicate.
        /// </returns>
        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return this.All().Where(predicate);
        }

        /// <summary>
        /// Projects each element of a sequence into a new form.
        /// </summary>
        /// <param name="predicate">
        /// A sequence of values to project.
        /// </param>
        /// <typeparam name="TResult">
        /// The type of the elements of source.
        /// </typeparam>
        /// <returns>
        /// An <see cref="IQueryable"/> whose elements are the result of invoking a projection function on each element of source.
        /// </returns>
        public virtual IQueryable<TResult> Transform<TResult>(Expression<Func<TEntity, TResult>> predicate)
        {
            return this.All().Select(predicate);
        }

        /// <summary>
        /// Returns the first element of a sequence.
        /// </summary>
        /// <param name="predicate">
        /// An expression to return the first element of a condition.
        /// </param>
        /// <returns>
        /// The first element that satisfies the condition.
        /// </returns>
        public virtual TEntity First(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Filter(predicate).First();
        }

        public virtual TEntity? Find(params object[] keys)
        {
            return ((DbSet<TEntity>)this.All()).Find(keys);
        }

        public virtual ValueTask<TEntity?> FindAsync(params object[] keys)
        {
            return this.FindAsync(CancellationToken.None, keys);
        }

        public virtual ValueTask<TEntity?> FindAsync(CancellationToken token, params object[] keys)
        {
            return ((DbSet<TEntity>)this.All()).FindAsync(keys, token);
        }

        /// <summary>
        /// Returns the first element of a sequence.
        /// </summary>
        /// <param name="predicate">
        /// An expression to return the first element of a condition.
        /// </param>
        /// <returns>
        /// The first element that satisfies the condition.
        /// </returns>
        public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Filter(predicate).FirstAsync();
        }

        public TEntity Create()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Marks the entity as added.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The entity with its state marked as added.
        /// </returns>
        public virtual TEntity Create(TEntity entity)
        {
            this.Context.Add(entity);
            return entity;
        }

        /// <summary>
        /// Marks the entities as added.
        /// </summary>
        /// <param name="listEntity">
        /// The entity list.
        /// </param>
        public virtual async Task CreateRangeAsync(List<TEntity> listEntity)
        {
            await this.Context.AddRangeAsync(listEntity);
        }

        /// <summary>
        /// Marks the entity as deleted.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The entity with its state marked as deleted.
        /// </returns>
        public virtual TEntity Delete(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Deleted;
            return entity;
        }

        /// <summary>
        /// Marks the entity as modified.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The entity with its state marked as modified.
        /// </returns>
        public virtual TEntity Update(TEntity entity)
        {
            this.Context.Set<TEntity>().Attach(entity);

            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Modified;
            return entity;
        }

        /// <summary>
        /// Copy values of new entity to old entity, new entity is marked as detached, and old entity is marked as modified.
        /// </summary>
        /// <param name="oldEntity">
        /// The old entity.
        /// </param>
        /// <param name="newEntity">
        /// The new entity.
        /// </param>
        /// <returns>
        /// The modified entity.
        /// </returns>
        public virtual TEntity Update(TEntity oldEntity, TEntity newEntity)
        {
            var newEntry = this.Context.Entry(newEntity);
            newEntry.State = EntityState.Detached;

            var oldEntry = this.Context.Entry(oldEntity);
            oldEntry.State = EntityState.Modified;

            return newEntity;
        }

        /// <summary>
        /// Marks the entities as modified.
        /// </summary>
        /// <param name="listEntity">
        /// The list Enetities.
        /// </param>
        /// <returns>
        /// The entity with its state marked as modified.
        /// </returns>
        public async Task<List<TEntity>> UpdateRangeAsync(List<TEntity> listEntity)
        {
            try
            {
                this.Context.Set<TEntity>().UpdateRange(listEntity);
                await this.Context.SaveChangesAsync();

                return listEntity;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al procesar la entidad.", ex);
            }
        }


        public Task<int> SaveChangesAsync()
        {
            return this.Context.SaveChangesAsync();
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns></returns>
        public virtual int SaveChanges()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the first element of the entity set with the specified condition, or a default value if the entity set contains no elements.
        /// </summary>
        /// <param name="predicate">
        /// A function to test each element for a condition.
        /// </param>
        /// <returns>
        /// default value, if the source is empty; otherwise, the first element in source.
        /// </returns>
        public virtual TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return this.All().Where(predicate).FirstOrDefault();
        }

        /// <summary>
        /// Returns the first element of the entity set with the specified condition, or a default value if the entity set contains no elements.
        /// </summary>
        /// <param name="predicate">
        /// A function to test each element for a condition.
        /// </param>
        /// <returns>
        /// default value, if the source is empty; otherwise, the first element in source.
        /// </returns>
        public virtual Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.All().Where(predicate).FirstOrDefaultAsync();
        }

        public void SetOriginalValue<TValue>(TEntity entity, string propertyName, TValue value)
        {
            this.Context.Entry(entity).OriginalValues[propertyName] = value;
        }
    }
}
