using Reto1_CleanArchitecture.Infrastructure.Context;

namespace Reto1_CleanArchitecture.Infrastructure.Repository
{
    public class RepositoryGenericReto1<TEntity> : RepositoryGeneric<TEntity, Reto1_DBContext>
        where TEntity : class
    {
        public RepositoryGenericReto1(Reto1_DBContext context)
            : base(context)
        {
        }
    }
}
