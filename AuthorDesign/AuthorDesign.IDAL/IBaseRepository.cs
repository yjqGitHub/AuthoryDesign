using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.IDAL {

    public interface IBaseRepository<T> where T : class,new() {
        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T AddEntity(T entity);
        /// <summary>
        /// 修改一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="property">需要修改的字段名称</param>
        /// <returns></returns>
        bool EditEntity(T entity, string[] property);
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool DeleteEntity(T entity);
        /// <summary>
        /// 查询
        /// </summary>
        IQueryable<T> LoadEntities();
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="whereLamda">查询条件</param>
        /// <returns></returns>
        IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLamda);
        /// <summary>
        /// 对查询结果进行升序排序
        /// </summary>
        /// <typeparam name="S">排序字段类型</typeparam>
        /// <param name="queryable">查询结果</param>
        /// <param name="orderLamda">排序表达式</param>
        /// <returns>根据排序条件排序好之后的排序结果</returns>
        IOrderedQueryable<T> Order<S>(IQueryable<T> queryable, Expression<Func<T, S>> orderLamda);
        /// <summary>
        /// 对排序结果再次进行升序排序
        /// </summary>
        /// <typeparam name="S">排序字段类型</typeparam>
        /// <param name="queryable">根据排序条件排序好之后的排序结果</param>
        /// <param name="orderLamda">排序表达式</param>
        /// <returns>根据排序条件排序好之后的排序结果</returns>
        IOrderedQueryable<T> ThenOrder<S>(IOrderedQueryable<T> queryable, Expression<Func<T, S>> orderLamda);
        /// <summary>
        /// 对查询结果进行降序排序
        /// </summary>
        /// <typeparam name="S">排序字段类型</typeparam>
        /// <param name="queryable">查询结果</param>
        /// <param name="orderLamda">排序表达式</param>
        /// <returns>根据排序条件排序好之后的排序结果</returns>
        IOrderedQueryable<T> OrderDesc<S>(IQueryable<T> queryable, Expression<Func<T, S>> orderLamda);
        /// <summary>
        /// 对排序结果再次进行降序排序
        /// </summary>
        /// <typeparam name="S">排序字段类型</typeparam>
        /// <param name="queryable">根据排序条件排序好之后的排序结果</param>
        /// <param name="orderLamda">排序表达式</param>
        /// <returns>根据排序条件排序好之后的排序结果</returns>
        IOrderedQueryable<T> ThenOrderDesc<S>(IOrderedQueryable<T> queryable, Expression<Func<T, S>> orderLamda);
        /// <summary>
        /// 对排序结果进行分页操作
        /// </summary>
        /// <param name="queryable">根据排序条件排序好之后的排序结果</param>
        /// <param name="nowNum">跳过序列中指定数量的元素</param>
        /// <param name="pageSize">从序列的开头返回指定数量的连续元素</param>
        /// <returns>指定长度的列表</returns>
        IQueryable<T> LoadPageEnties(IOrderedQueryable<T> queryable, int nowNum, int pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="S">排序类型</typeparam>
        /// <param name="whereLamda">查询条件</param>
        /// <param name="orderLamda">排序条件</param>
        /// <param name="isDesc">是否倒序</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">页长</param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        IQueryable<T> LoadEntities<S>(Expression<Func<T, bool>> whereLamda, Expression<Func<T, S>> orderLamda, bool isDesc, int pageIndex, int pageSize, out int rowCount);

        Boolean Get(Expression<Func<T, bool>> where);
    }
}
