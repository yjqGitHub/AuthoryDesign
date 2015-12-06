using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.DAL {
    public class BaseRepository<T> where T : class,new() {

        public DbContext db = DbContextFactory.GetCurrentDbContext();

        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T AddEntity(T entity) {
            db.Entry<T>(entity).State = EntityState.Added;
            //db.SaveChanges();
            return entity;
        }
        /// <summary>
        /// 修改一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="property">需要修改的字段名称</param>
        /// <returns></returns>
        public bool EditEntity(T entity, string[] property) {
            DbEntityEntry<T> entry = db.Entry<T>(entity);
            entry.State = EntityState.Unchanged;
            foreach (var item in property) {
                entry.Property(item).IsModified = true;
            }
            //return db.SaveChanges() > 0;
            return true;
        }
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteEntity(T entity) {
            DbEntityEntry<T> entry = db.Entry<T>(entity);
            entry.State = EntityState.Deleted;
            //return db.SaveChanges() > 0;
            return true;
        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> LoadEntities() {
            return db.Set<T>();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="whereLamda">查询条件</param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLamda) {
            return db.Set<T>().Where<T>(whereLamda);
        }
        /// <summary>
        /// 对查询结果进行升序排序
        /// </summary>
        /// <typeparam name="S">排序字段类型</typeparam>
        /// <param name="queryable">查询结果</param>
        /// <param name="orderLamda">排序表达式</param>
        /// <returns>根据排序条件排序好之后的排序结果</returns>
        public IOrderedQueryable<T> Order<S>(IQueryable<T> queryable, Expression<Func<T, S>> orderLamda) {
            return queryable.OrderBy(orderLamda);
        }
        /// <summary>
        /// 对排序结果再次进行升序排序
        /// </summary>
        /// <typeparam name="S">排序字段类型</typeparam>
        /// <param name="queryable">根据排序条件排序好之后的排序结果</param>
        /// <param name="orderLamda">排序表达式</param>
        /// <returns>根据排序条件排序好之后的排序结果</returns>
        public IOrderedQueryable<T> ThenOrder<S>(IOrderedQueryable<T> queryable, Expression<Func<T, S>> orderLamda) {
            return queryable.ThenBy(orderLamda);
        }
        /// <summary>
        /// 对查询结果进行降序排序
        /// </summary>
        /// <typeparam name="S">排序字段类型</typeparam>
        /// <param name="queryable">查询结果</param>
        /// <param name="orderLamda">排序表达式</param>
        /// <returns>根据排序条件排序好之后的排序结果</returns>
        public IOrderedQueryable<T> OrderDesc<S>(IQueryable<T> queryable, Expression<Func<T, S>> orderLamda) {
            return queryable.OrderByDescending(orderLamda);
        }
        /// <summary>
        /// 对排序结果再次进行降序排序
        /// </summary>
        /// <typeparam name="S">排序字段类型</typeparam>
        /// <param name="queryable">根据排序条件排序好之后的排序结果</param>
        /// <param name="orderLamda">排序表达式</param>
        /// <returns>根据排序条件排序好之后的排序结果</returns>
        public IOrderedQueryable<T> ThenOrderDesc<S>(IOrderedQueryable<T> queryable, Expression<Func<T, S>> orderLamda) {
            return queryable.ThenByDescending(orderLamda);
        }
        /// <summary>
        /// 对排序结果进行分页操作
        /// </summary>
        /// <param name="queryable">根据排序条件排序好之后的排序结果</param>
        /// <param name="nowNum">跳过序列中指定数量的元素</param>
        /// <param name="pageSize">从序列的开头返回指定数量的连续元素</param>
        /// <returns>指定长度的列表</returns>
        public IQueryable<T> LoadPageEnties(IOrderedQueryable<T> queryable, int nowNum, int pageSize) {
            return queryable.Skip<T>(nowNum + 1).Take<T>(pageSize);
        }
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
        public IQueryable<T> LoadEntities<S>(Expression<Func<T, bool>> whereLamda, Expression<Func<T, S>> orderLamda, bool isDesc, int pageIndex, int pageSize, out int rowCount) {
            var temp = db.Set<T>().Where<T>(whereLamda);
            rowCount = temp.Count();
            if (isDesc)
                temp = temp.OrderByDescending<T, S>(orderLamda).Skip<T>(pageSize * (pageIndex - 1) + 1).Take<T>(pageSize);
            else
                temp = temp.OrderBy<T, S>(orderLamda).Skip<T>(pageSize * (pageIndex - 1) + 1).Take<T>(pageSize);
            return temp;
        }

        public virtual Boolean Get(Expression<Func<T, bool>> where) {
            return RemoveHoldingEntityInContext(db.Set<T>().Where(where).AsNoTracking().FirstOrDefault<T>());
        }

        public virtual IQueryable<T> GetMany(Expression<Func<T, bool>> where) {
            return db.Set<T>().Where(where).AsNoTracking();
        }
        //用于监测Context中的Entity是否存在，如果存在，将其Detach，防止出现问题。
        private Boolean RemoveHoldingEntityInContext(T entity) {
            var objContext = ((IObjectContextAdapter)db).ObjectContext;
            var objSet = objContext.CreateObjectSet<T>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            Object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);
            if (exists) {
                objContext.Detach(foundEntity);
            }
            return (exists);
        }
    }
}
