using AuthorDesign.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.IDAL {
    public interface IPageMenuRepository : IBaseRepository<PageMenu> {
        /// <summary>
        /// 获取菜单按钮集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<PageMenuAction> GetPageMenuList();
        /// <summary>
        /// 根据角色Id获取可设置的页面列表
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        IEnumerable<PageMenuAction> GetAdminPageMenuList(int roleId);
        /// <summary>
        /// 获取超级管理员显示菜单列表
        /// </summary>
        /// <returns></returns>
        IQueryable<PageMenu> GetSuperAdminShowPage();
        /// <summary>
        /// 获取管理员可执行操作页面
        /// </summary>
        /// <param name="adminId">管理员Id</param>
        /// <returns></returns>
        List<AdminPageAction> GetAdminShowPage(int adminId);
    }
}
