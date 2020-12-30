using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using GIS.Authority.Entity.Base;
using GIS.Authority.Common;

namespace GIS.Authority.Dal.Base.IBaseDal
{
    /// <summary>
    /// 仓储基类接口
    /// </summary>
    /// <typeparam name="T">基类泛型</typeparam>
    public interface IBasicRepository<T>
    {
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns>整张表数据</returns>
        List<T> GetList();

        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="pg">pg</param>
        /// <returns>List<T></returns>
        List<T> GetList(PredicateGroup pg);
        IList<T> GetPager(PredicateGroup group, PageQuery query);
        /// <summary>
        /// 数目
        /// </summary>
        /// <param name="pg">pg</param>
        /// <param name="conn">conn</param>
        /// <returns>int</returns>
        int Count(object pg, IDbConnection conn = null);

        /// <summary>
        /// 通过主键id获取数据
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns>该条数据</returns>
        T Get(Guid id);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="t">更新实体</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>更新是否成功</returns>
        bool Update(T t, IDbConnection conn = null);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t">新增实体</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>新增实体</returns>
        T Insert(T t, IDbConnection conn = null);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="t">删除实体</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>删除是否成功</returns>
        bool Delete(T t, IDbConnection conn = null);

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>执行影响的语句</returns>
        bool Execute(string sql, object param = null, IDbConnection conn = null);

        /// <summary>
        /// 通过查询条件获取全部数据
        /// </summary>
        /// <param name="pg">查询条件,需要自己在Service层构造</param>
        /// <param name="orderList">排序方式，默认id倒序</param>
        /// <returns>查询结果数组</returns>
        List<T> Find(PredicateGroup pg, List<OrderItem> orderList = null);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pg">查询方式,需要自己在Service层构造</param>
        /// <param name="pageQuery">分页条件</param>
        /// <returns>查询分页结果</returns>
        PageResult<T> FindByPage(PredicateGroup pg, PageQuery pageQuery);

        /// <summary>
        /// 事务封装（无返回结果）,方法内的增删改都必须将conn作为参数带到仓储中执行
        /// </summary>
        /// <param name="action">方法</param>
        void Transaction(Action<IDbConnection> action);

        /// <summary>
        /// 事务封装 （有返回结果）,方法内的增删改都必须将conn作为参数带到仓储中执行
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="funData">方法</param>
        /// <returns>事务封装方法结果</returns>
        TResult Transaction<TResult>(Func<IDbConnection, TResult> funData);

        /// <summary>
        /// 判断表是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>是否存在</returns>
        bool IsTableExists(string tableName);
    }
}