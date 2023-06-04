namespace Blog.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDBContext
    {
        IDBTransaction BeginTrainsaction();

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        //int ExecuteSqlCommand(string sql, params object[] parameters);

        //void SqlBulkInsert(string tableName, Dictionary<string, string> mapping, DataTable data, int batchSize = 10000, IDBTransaction transaction = null);
    }
}