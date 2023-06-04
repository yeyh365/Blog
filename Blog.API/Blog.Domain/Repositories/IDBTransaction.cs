namespace Blog.Domain.Repositories
{
    using System;

    public interface IDBTransaction: IDisposable
    {
        void Commit();

        void Rollback();
    }
}