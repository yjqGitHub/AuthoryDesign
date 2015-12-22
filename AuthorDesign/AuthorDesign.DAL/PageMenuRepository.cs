using AuthorDesign.IDAL;
using AuthorDesign.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.DAL {
    public class PageMenuRepository : BaseRepository<PageMenu>, IPageMenuRepository {
        /// <summary>
        /// 获取菜单按钮集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PageMenuAction> GetPageMenuList() {
            var result = from p in db.Set<PageMenu>()
                         join a in db.Set<ActionToPage>() on new { PageId = p.Id } equals new { PageId = a.PageId } into a_into
                         from a in a_into.DefaultIfEmpty()
                         orderby p.PId, p.OrderNum
                         select new {
                             p.Id,
                             p.Name,
                             p.PId,
                             a.ActionList,
                             IsDelete = a.IsDelete == null ? (byte)0 : a.IsDelete
                         };
            return result.ToList().Select(m => new PageMenuAction() { ActionList = m.ActionList, Id = m.Id, IsDelete = m.IsDelete, Name = m.Name, PId = m.PId });
        }
        /// <summary>
        /// 根据角色Id获取可设置的页面列表
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public IEnumerable<PageMenuAction> GetAdminPageMenuList(int roleId) {
            var result = from p in db.Set<PageMenu>()
                         join a in db.Set<ActionToPage>() on new { PageId = p.Id } equals new { PageId = a.PageId } into a_into
                         from a in a_into.DefaultIfEmpty()
                         join b in db.Set<AuthoryToPage>() on new { PageId = p.Id } equals new { PageId = b.PageId } into b_into
                         from b in b_into
                         where b.IsShow == 1&&b.AuthoryId==roleId
                         orderby p.PId, p.OrderNum
                         select new {
                             p.Id,
                             p.Name,
                             p.PId,
                             b.ActionList,
                             IsDelete = b.IsDelete == null ? (byte)0 : b.IsDelete
                         };
            return result.ToList().Select(m => new PageMenuAction() { ActionList = m.ActionList, Id = m.Id, IsDelete = m.IsDelete, Name = m.Name, PId = m.PId });
        }
        /// <summary>
        /// 获取超级管理员显示菜单列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<PageMenu> GetSuperAdminShowPage() {
            var result = from p in db.Set<PageMenu>()
                         where p.IsShow == 1
                         orderby p.PId, p.OrderNum
                         select p;
            return result;
        }
        /// <summary>
        /// 获取管理员可执行操作页面
        /// </summary>
        /// <param name="adminId">管理员Id</param>
        /// <returns></returns>
        public List<AdminPageAction> GetAdminShowPage(int adminId) {
            var result = from a in db.Set<PageMenu>()
                         join K in
                             (
                                 (from c in db.Set<AdminToPage>()
                                  join d in db.Set<AuthoryToPage>() on c.PageId equals d.PageId
                                  where
                                    c.AdminId == adminId &&
                                    d.IsShow == 1 &&
                                    d.AuthoryId ==
                                      ((from admins in db.Set<Admin>()
                                        where
                                          admins.Id == adminId
                                        select new {
                                            admins.AuthoryId
                                        }).FirstOrDefault().AuthoryId)
                                  select new {
                                      PageId = c.PageId,
                                      AdminActionList = c.ActionList,
                                      RoleActionList = d.ActionList
                                  })) on new { Id = a.Id } equals new { Id = K.PageId }
                         select new {
                             a.Id,
                             a.PId,
                             a.PageUrl,
                             a.OrderNum,
                             a.Name,
                             a.Ico,
                             K.AdminActionList,
                             K.RoleActionList
                         };
            var res= result.ToList();
            return res.Select(a => new AdminPageAction() {
                Id = a.Id,
                PId = a.PId,
                PageUrl = a.PageUrl,
                OrderNum = a.OrderNum,
                Name = a.Name,
                Ico = a.Ico,
                AdminActionList = a.AdminActionList,
                RoleActionList = a.RoleActionList
            }).ToList();
        }
    }
}
