using AuthorDesign.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.DAL {
        /// <summary>
    /// 仓储入口
    /// </summary>
    public class RepositoryEnter:IRepositoryEnter {
        /// <summary>
        /// 统一SaveChange方法
        /// </summary>
        /// <returns></returns>
        public int SaveChange() {
            return DbContextFactory.GetCurrentDbContext().SaveChanges();

        }
        /// <summary>
        /// 获取页面与页面动作联系仓储
        /// </summary>
        public IDAL.IActionToPageRepository GetActionToPageRepository { get { return new ActionToPageRepository(); } }
        /// <summary>
        /// 获取管理员登录日志仓储
        /// </summary>
        public IDAL.IAdminLoginLogRepository GetAdminLoginLogRepository { get { return new AdminLoginLogRepository(); } }
        /// <summary>
        /// 获取管理员操作仓储
        /// </summary>
        public IDAL.IAdminOperationRepository GetAdminOperationRepository { get { return new AdminOperationRepository(); } }
        /// <summary>
        /// 获取管理员仓储
        /// </summary>
        public IDAL.IAdminRepository GetAdminRepository { get { return new AdminRepository(); } }
        /// <summary>
        /// 获取管理员与页面仓储
        /// </summary>
        public IDAL.IAdminToPageRepository GetAdminToPageRepository { get { return new AdminToPageRepository(); } }
        /// <summary>
        /// 获取角色仓储
        /// </summary>
        public IDAL.IAuthoryRepository GetAuthoryRepository { get { return new AuthoryRepository(); } }
        /// <summary>
        /// 获取角色与页面仓储
        /// </summary>
        public IDAL.IAuthoryToPageRepository GetAuthoryToPageRepository { get { return new AuthoryToPageRepository(); } }
        /// <summary>
        /// 获取页面动作仓储
        /// </summary>
        public IDAL.IPageActionRepository GetPageActionRepository { get { return new PageActionRepository(); } }
        /// <summary>
        /// 获取页面仓储
        /// </summary>
        public IDAL.IPageMenuRepository GetPageMenuRepository { get { return new PageMenuRepository(); } }
    }
}
