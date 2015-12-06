using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.IDAL {
    /// <summary>
    /// 仓储入口
    /// </summary>
    public interface IRepositoryEnter {
        /// <summary>
        /// 统一SaveChange方法
        /// </summary>
        /// <returns></returns>
        int SaveChange();
        /// <summary>
        /// 获取页面与页面动作联系仓储
        /// </summary>
        IDAL.IActionToPageRepository GetActionToPageRepository { get; }
        /// <summary>
        /// 获取管理员登录日志仓储
        /// </summary>
        IDAL.IAdminLoginLogRepository GetAdminLoginLogRepository { get; }
        /// <summary>
        /// 获取管理员操作仓储
        /// </summary>
        IDAL.IAdminOperationRepository GetAdminOperationRepository { get; }
        /// <summary>
        /// 获取管理员仓储
        /// </summary>
        IDAL.IAdminRepository GetAdminRepository { get; }
        /// <summary>
        /// 获取管理员与页面仓储
        /// </summary>
        IDAL.IAdminToPageRepository GetAdminToPageRepository { get; }
        /// <summary>
        /// 获取角色仓储
        /// </summary>
        IDAL.IAuthoryRepository GetAuthoryRepository { get; }
        /// <summary>
        /// 获取角色与页面仓储
        /// </summary>
        IDAL.IAuthoryToPageRepository GetAuthoryToPageRepository { get; }
        /// <summary>
        /// 获取页面动作仓储
        /// </summary>
        IDAL.IPageActionRepository GetPageActionRepository { get; }
        /// <summary>
        /// 获取页面仓储
        /// </summary>
        IDAL.IPageMenuRepository GetPageMenuRepository { get; }
    }
}
