namespace Blog.EntityFramework.Repositories
{
    using Blog.Domain.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;

    public class EFDBTransaction : IDBTransaction
    {
        private IDbContextTransaction _transaction;

        public EFDBTransaction(DbContext context)
        {
            _transaction = context.Database.BeginTransaction();
        }

        public void Commit()
        {
            this._transaction.Commit();
        }

        public void Rollback()
        {
            this._transaction.Rollback();
        }

        public void Dispose()
        {
            this._transaction.Dispose();
        }

        public IDbContextTransaction GetTransaction()
        {
            return _transaction;
        }
    }
}