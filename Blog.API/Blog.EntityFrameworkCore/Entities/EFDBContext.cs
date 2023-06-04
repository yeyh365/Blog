using Blog.Core.Helper;
using Blog.Domain.DBFilter;
using Blog.Domain.Entities;
using Blog.Domain.Repositories;
using Blog.EntityFramework.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrainingPlaning.Core.Helper;

namespace Blog.EntityFrameworkCore
{
    public class EFDBContext : DbContext, IDBContext
    {
        private const string DeleteFilter = "DeleteFilter";
        private readonly IHttpContextAccessor _httpContext;


        public EFDBContext(DbContextOptions<EFDBContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContext = httpContextAccessor;
            //在此可对数据库连接字符串做加解密操作
        }

        #region Entities
        //管理员
        public DbSet<User> User { get; set; }
        //联系我们
        public DbSet<ContactUs> ContactUs { get; set; }
        //日志
        public DbSet<Log> Log { get; set; }

        //字典表
        public DbSet<Dictionary> Dictionary { get; set; }
        //关键字表
        public DbSet<Keywords> Keywords { get; set; }
        //资源表
        public DbSet<Material> Material { get; set; }
        //文章资源关键字关联表
        public DbSet<MaterialArticleKeywords> MaterialKeywords { get; set; }
        //文章表
        public DbSet<Article> Article { get; set; }
        //互动信息表
        public DbSet<Interaction> Interaction { get; set; }
        //互动信息表
        public DbSet<Comment> Comment { get; set; }


        #endregion

        public IDBTransaction BeginTrainsaction()
        {
            return new EFDBTransaction(this);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //全局过滤器，过滤Deleted=FALSE的数据
            var entityTypeList = modelBuilder.Model.GetEntityTypes().Where(x => typeof(IDeleteFilter).IsAssignableFrom(x.ClrType));
            foreach (var entityType in entityTypeList)
            {
                var parameter = Expression.Parameter(entityType.ClrType);
                var propertyMethodInfo = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(bool));
                var isDeletedProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant("Deleted"));

                BinaryExpression compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty, Expression.Constant(false));

                var lambdaExpression = Expression.Lambda(compareExpression, parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambdaExpression);
            }
        }

        /// 开启删除过滤
        /// </summary>
        public void EnableDeleteFilter()
        {
            this.EnableDeleteFilter();
        }
        /// <summary>
        /// 取消删除的全局过滤
        /// </summary>
        public void DisableActiveFilter()
        {
            this.DisableActiveFilter();
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries();
            foreach (var ent in entries)
            {
                switch (ent.State)
                {
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Added:
                        HandleInsert(ent);
                        break;
                    case EntityState.Modified:
                        HandleUpdate(ent);
                        break;
                    case EntityState.Deleted:
                        HandleDelete(ent);
                        break;
                    default:
                        break;
                }
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var entries = ChangeTracker.Entries();
            foreach (var ent in entries)
            {
                switch (ent.State)
                {
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Added:
                        HandleInsert(ent);
                        break;
                    case EntityState.Modified:
                        HandleUpdate(ent);
                        break;
                    case EntityState.Deleted:
                        HandleDelete(ent);
                        break;
                    default:
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        private void HandleInsert(EntityEntry entry)
        {
            var currentUserId = UserAd;
            if (entry.Entity is IEntityCreate)
            {
                var entity = entry.Entity as IEntityCreate;
                if (!entity.Created.HasValue)
                {
                    entity.Created = DateTime.Now;
                }
                if (string.IsNullOrEmpty(entity.CreatedBy))
                {
                    entity.CreatedBy = currentUserId;
                }
            }
            if (entry.Entity is IEntityUpdate)
            {
                var entity = entry.Entity as IEntityUpdate;
                entity.Updated = DateTime.Now;
                entity.UpdatedBy = currentUserId;
            }
        }

        private void HandleUpdate(EntityEntry entry)
        {
            var currentUserId = UserAd;
            if (entry.Entity is IEntityCreate)
            {
                entry.Property("Created").IsModified = false;
                entry.Property("CreatedBy").IsModified = false;
            }
            if (entry.Entity is IEntityUpdate)
            {
                var entity = entry.Entity as IEntityUpdate;
                entity.Updated = DateTime.Now;
                entity.UpdatedBy = currentUserId;
            }
        }

        private void HandleDelete(EntityEntry entry)
        {

        }

        public string UserAd
        {
            get
            {
                var User = _httpContext.HttpContext.User.Claims.Where(x => x.Type == "Phone").FirstOrDefault();
                if (User==null)
                {
                    return "";
                }
                else
                {
                    return User.Value;
                }
            }
        }


        //public int ExecuteSqlCommand(string sql, params object[] parameters)
        //{
        //    var result = this.Database.(sql, parameters);
        //    return result;
        //}

        ///// <summary>
        ///// 批量导入
        ///// </summary>
        ///// <param name="tableName"></param>
        ///// <param name="mapping">data.columnName:db.columnName</param>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public void SqlBulkInsert(string tableName, Dictionary<string, string> mapping, DataTable data, int batchSize = 10000, IDBTransaction transaction = null)
        //{
        //    if (data != null && data.Rows.Count > 0)
        //    {
        //        var conn = this.Database as SqlConnection;
        //        if (conn.State != ConnectionState.Open)
        //        {
        //            conn.Open();
        //        }
        //        SqlTransaction tran = null;
        //        if (transaction != null)
        //        {
        //            //暂取消事务
        //            //tran = (transaction as EFDBTransaction).GetTransaction() ;
        //        }
        //        //using (conn)//和外部共用一个connection，不能释放
        //        {
        //            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, tran))
        //            {
        //                bulkCopy.DestinationTableName = tableName;

        //                foreach (KeyValuePair<string, string> p in mapping)
        //                {
        //                    bulkCopy.ColumnMappings.Add(p.Key, p.Value);
        //                }

        //                bulkCopy.BatchSize = batchSize;
        //                bulkCopy.WriteToServer(data);
        //            }
        //            conn.Close();
        //        }
        //    }
        //}
    }
}
