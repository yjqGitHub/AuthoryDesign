using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.DAL {
    /// <summary>
    /// 当前线程内的数据上下文
    /// </summary>
    public class DbContextFactory {
        /// <summary>
        /// 获取当前线程内的数据上下文，如果当前线程内没有上下文，那么创建一个上下文，
        /// </summary>
        /// <returns>当前线程内的数据上下文</returns>
        public static DbContext GetCurrentDbContext() {
            DbContext currentContext = CallContext.GetData("CurrentDbContext") as DbContext;
            if (currentContext == null) {
                currentContext = new AuthorDesignContext();
                CallContext.SetData("CurrentDbContext", currentContext);
            }
            return currentContext;
        }
    }
}
