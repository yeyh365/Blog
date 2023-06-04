namespace Blog.Domain.Repositories
{
    public interface IRepositoryProvider
    {
        IRepository<TEntity, TPrimaryKey> Create<TEntity, TPrimaryKey>(IDBContext context)
            where TEntity : class, IEntitiy<TPrimaryKey>;

        IRepository<TEntity> Create<TEntity>(IDBContext context)
           where TEntity : class, IEntitiy<int>;
    }
}