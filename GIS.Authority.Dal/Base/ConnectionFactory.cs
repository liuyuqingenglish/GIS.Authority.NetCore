using DapperExtensions.Sql;
using Npgsql;
using System.Data;
using GIS.Authority.Common;
namespace GIS.Authority.Dal.Base
{
    /// <summary>
    /// 返回数据库连接
    /// </summary>
    internal class ConnectionFactory
    {
        /// <summary>
        /// The connection str.
        /// </summary>
        private static readonly string ConnectionStr = ConfigurationData.ConnectionStr;

        /// <summary>
        /// 创建连接
        /// </summary>
        /// <returns>连接</returns>
        public static IDbConnection CreateConnection()
        {
            DapperExtensions.DapperExtensions.SqlDialect = new PostgreSqlDialect();
            var connection = new NpgsqlConnection(ConnectionStr);
            return connection;
        }

        /// <summary>
        /// 创建超长时间连接
        /// </summary>
        /// <returns>连接</returns>
        public static IDbConnection CreateLongTimeConnection()
        {
            DapperExtensions.DapperExtensions.SqlDialect = new PostgreSqlDialect();
            var connection = new NpgsqlConnection($"{ConnectionStr}commandTimeout=1000;");
            return connection;
        }
    }
}