using Dapper;
using DapperExtensions;
using GIS.Authority.Common;
using GIS.Authority.Dal.Base.IBaseDal;
using GIS.Authority.Entity.Base.BaseEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GIS.Authority.Dal.Base.BaseDal
{
    /// <summary>
    /// 仓库基类
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <typeparam name="TE">实体泛型</typeparam>
    public class BasicRepository<T> : IBasicRepository<T> where T : BaseEntity<Guid>, new()
    {
        /// <summary>
        /// 表名
        /// </summary>
        public static readonly string TableName = new DapperExtensionsConfiguration().GetMap<T>().TableName;

        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns>整张表数据</returns>
        public virtual List<T> GetList()
        {
            using (IDbConnection conn = ConnectionFactory.CreateConnection())
            {
                return conn.GetList<T>().ToList();
            }
        }

        public bool Delete(PredicateGroup group)
        {
            using (IDbConnection conn = ConnectionFactory.CreateConnection())
            {
                return conn.Delete<T>(group);
            }
        }

        public T Get(string sql)
        {
            using (IDbConnection conn = ConnectionFactory.CreateConnection())
            {
                conn.Open();
                conn.BeginTransaction();
                IDataReader reader = conn.ExecuteReader(sql);
                DataTable table = reader.GetSchemaTable();
                return DataConvertExtension<T>.Get(table);
            }
        }

        public PageResult<T> GetPager(PredicateGroup pg, PageQuery pageQuery)
        {
            var result = new PageResult<T>();
            ////PageIndex等于0时返回全部
            if (pageQuery.PageIndex == 0)
            {
                using (var conn = ConnectionFactory.CreateConnection())
                {
                    result.Row = conn.GetList<T>(pg, SortExtension.ToSortList<T>(pageQuery.OrderList)).ToList();
                    result.Total = result.Row.Count;
                    return result;
                }
            }

            if (pageQuery.PageIndex < 0)
            {
                throw new ArgumentException(nameof(pageQuery.PageIndex));
            }

            if (pageQuery.PageSize <= 0)
            {
                throw new ArgumentException(nameof(pageQuery.PageSize));
            }

            using (var conn = ConnectionFactory.CreateConnection())
            {
                var total = conn.Count<T>(pg);
                result.Total = total;

                //判断请求有数据时再查询
                if (total > (pageQuery.PageIndex - 1) * pageQuery.PageSize)
                {
                    result.Row = conn.GetPage<T>(pg, SortExtension.ToSortList<T>(pageQuery.OrderList), pageQuery.PageIndex - 1, pageQuery.PageSize)
                        .ToList();
                }

                return result;
            }
        }

        /// <summary>
        /// 通过主键id获取数据
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns>该条数据</returns>
        public virtual T Get(Guid id)
        {
            using (var conn = ConnectionFactory.CreateConnection())
            {
                return conn.Get<T>(id);
            }
        }

        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="pg">pg</param>
        /// <returns>List<T></returns>
        public virtual List<T> GetList(PredicateGroup pg)
        {
            using (var conn = ConnectionFactory.CreateConnection())
            {
                return conn.GetList<T>(pg).ToList();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="t">更新实体</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>更新是否成功</returns>
        public virtual bool Update(T t, IDbConnection conn = null)
        {
            if (conn != null)
            {
                return conn.Update(t);
            }

            using (conn = ConnectionFactory.CreateConnection())
            {
                return conn.Update(t);
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t">新增实体</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>新增实体</returns>
        public virtual T Insert(T t, IDbConnection conn = null)
        {
            if (conn != null)
            {
                conn.Insert(t);
                return t;
            }

            using (conn = ConnectionFactory.CreateConnection())
            {
                conn.Insert(t);
                return t;
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t">新增实体</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>新增实体</returns>
        public bool Insert(T t)
        {
            using (var conn = ConnectionFactory.CreateConnection())
            {
                conn.Insert(t);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="t">删除实体</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>删除是否成功</returns>
        public virtual bool Delete(T t, IDbConnection conn = null)
        {
            if (conn != null)
            {
                return conn.Delete(t);
            }

            using (conn = ConnectionFactory.CreateConnection())
            {
                return conn.Delete(t);
            }
        }

        /// <summary>
        /// 数目
        /// </summary>
        /// <param name="pg">pg</param>
        /// <param name="conn">conn</param>
        /// <returns>int</returns>
        public virtual int Count(object pg, IDbConnection conn = null)
        {
            if (conn != null)
            {
                return conn.Count<T>(pg);
            }

            using (conn = ConnectionFactory.CreateConnection())
            {
                return conn.Count<T>(pg);
            }
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>执行影响的语句</returns>
        public virtual bool Execute(string sql, object param = null, IDbConnection conn = null)
        {
            if (conn != null)
            {
                return conn.Execute(sql, param) > 0;
            }

            using (conn = ConnectionFactory.CreateConnection())
            {
                return conn.Execute(sql, param) > 0;
            }
        }

        /// <summary>
        /// 通过查询条件获取全部数据
        /// </summary>
        /// <param name="pg">查询条件,需要自己在Service层构造</param>
        /// <param name="orderList">排序方式，默认id倒序</param>
        /// <returns>查询结果数组</returns>
        public virtual List<T> Find(PredicateGroup pg, List<OrderItem> orderList = null)
        {
            using (var conn = ConnectionFactory.CreateConnection())
            {
                return conn.GetList<T>(pg, SortExtension.ToSortList<T>(orderList)).ToList();
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pg">查询方式,需要自己在Service层构造</param>
        /// <param name="pageQuery">分页条件</param>
        /// <returns>查询分页结果</returns>
        public virtual PageResult<T> FindByPage(PredicateGroup pg, PageQuery pageQuery)
        {
            var result = new PageResult<T>();
            ////PageIndex等于0时返回全部
            if (pageQuery.PageIndex == 0)
            {
                using (var conn = ConnectionFactory.CreateConnection())
                {
                    result.Row = conn.GetList<T>(pg, SortExtension.ToSortList<T>(pageQuery.OrderList)).ToList();
                    result.Total = result.Row.Count;
                    return result;
                }
            }

            if (pageQuery.PageIndex < 0)
            {
                throw new ArgumentException(nameof(pageQuery.PageIndex));
            }

            if (pageQuery.PageSize <= 0)
            {
                throw new ArgumentException(nameof(pageQuery.PageSize));
            }

            using (var conn = ConnectionFactory.CreateConnection())
            {
                var total = conn.Count<T>(pg);
                result.Total = total;

                //判断请求有数据时再查询
                if (total > (pageQuery.PageIndex - 1) * pageQuery.PageSize)
                {
                    result.Row = conn.GetPage<T>(pg, SortExtension.ToSortList<T>(pageQuery.OrderList), pageQuery.PageIndex - 1, pageQuery.PageSize)
                        .ToList();
                }

                return result;
            }
        }

        /// <summary>
        /// 事务封装（无返回结果）,方法内的增删改都必须将conn作为参数带到仓储中执行
        /// </summary>
        /// <param name="action">方法</param>
        public void Transaction(Action<IDbConnection> action)
        {
            using (var conn = ConnectionFactory.CreateLongTimeConnection())
            {
                conn.Open();
                var transaction = conn.BeginTransaction();
                try
                {
                    action(conn);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 事务封装 （有返回结果）,方法内的增删改都必须将conn作为参数带到仓储中执行
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="funData">方法</param>
        /// <returns>事务封装方法结果</returns>
        public TResult Transaction<TResult>(Func<IDbConnection, TResult> funData)
        {
            using (var conn = ConnectionFactory.CreateLongTimeConnection())
            {
                conn.Open();
                var transaction = conn.BeginTransaction();
                try
                {
                    var result = funData(conn);
                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 判断表是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>是否存在</returns>
        public bool IsTableExists(string tableName)
        {
            var sql = $"SELECT COUNT(1) FROM pg_class WHERE UPPER(RelName) = UPPER(@tableName)";
            using (var conn = ConnectionFactory.CreateConnection())
            {
                var count = conn.ExecuteScalar<int>(sql, new { tableName });
                return count > 0;
            }
        }

        public List<TResult> GetList<TResult>(string sql) where TResult : new()
        {
            using (IDbConnection conn = ConnectionFactory.CreateConnection())
            {
                conn.Open();
                conn.BeginTransaction();
                IDataReader reader = conn.ExecuteReader(sql);
                DataTable table = reader.GetSchemaTable();
                return DataConvertExtension<TResult>.GetList(table);
            }
        }

        public TResult Get<TResult>(string sql) where TResult : new()
        {
            using (IDbConnection conn = ConnectionFactory.CreateConnection())
            {
                conn.Open();
                conn.BeginTransaction();
                IDataReader reader = conn.ExecuteReader(sql);
                DataTable table = reader.GetSchemaTable();
                return DataConvertExtension<TResult>.Get(table);
            }
        }

        public List<TResult> TransactionResult<TResult>(string sql) where TResult : new()
        {
            using (var conn = ConnectionFactory.CreateConnection())
            {
                conn.Open();
                var transaction = conn.BeginTransaction();
                try
                {
                    IDataReader reader = conn.ExecuteReader(sql);
                    transaction.Commit();
                    return new List<TResult>();
                }
                catch
                {
                    transaction.Rollback();
                    return new List<TResult>();
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}