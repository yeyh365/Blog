namespace Blog.EntityFramework.Repositories
{
    using Blog.Domain.Entities;
    using Blog.Domain.Repositories;

    public class EFRepositoryProvider : IRepositoryProvider
    {
        public IRepository<TEntity, TPrimaryKey> Create<TEntity, TPrimaryKey>(IDBContext context)
            where TEntity : class, IEntitiy<TPrimaryKey>
        {
            return new EFRepository<TEntity, TPrimaryKey>(context);
        }

        public IRepository<TEntity> Create<TEntity>(IDBContext context)
            where TEntity : class, IEntitiy<int>
        {
            return new EFRepository<TEntity>(context);
        }
    }
}