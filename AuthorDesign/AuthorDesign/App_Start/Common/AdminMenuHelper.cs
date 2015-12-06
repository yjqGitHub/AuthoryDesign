using AuthorDesign.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthorDesign.Web.App_Start.Common {
    /// <summary>
    /// 管理员菜单帮助类
    /// </summary>
    public class AdminMenuHelper {
        /// <summary>
        /// 获取可执行的页面列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.PageMenu> GetOperationMenu() {
            if (WebCookieHelper.AdminCheckLogin()) {
                if (WebCookieHelper.GetAdminId(5) == 1) {// 获取超级管理员的可执行页面
                    return GetSuperAdminMenu();
                }
                else {// 获取管理员可操作菜单列表
                   return GetAdminMenu(WebCookieHelper.GetAdminId(0)).Select(m => new Model.PageMenu() { PId=m.PId, PageUrl=m.PageUrl, Id=m.Id, Ico=m.Ico, Name=m.Name, OrderNum=m.OrderNum }).ToList();
                }
            } else {
                return new List<Model.PageMenu>();
            }
        }
        /// <summary>
        /// 获取超级管理员的可执行页面
        /// </summary>
        /// <returns></returns>
        public static List<Model.PageMenu> GetSuperAdminMenu() {
            if (CacheHelper.IsExistCache("SuperAdminMenuList")) {
                return CacheHelper.GetCache("SuperAdminMenuList") as List<Model.PageMenu>;
            }
            else {
                List<Model.PageMenu> superAdminMenuList = EnterRepository.GetRepositoryEnter().GetPageMenuRepository.GetSuperAdminShowPage().ToList();
                CacheHelper.AddCache("SuperAdminMenuList", superAdminMenuList, 1);
                return superAdminMenuList;
            }
        }

        /// <summary>
        /// 获取管理员可操作菜单列表
        /// </summary>
        /// <param name="adminId">管理员Id</param>
        /// <returns></returns>
        public static List<Model.AdminPageAction> GetAdminMenu(int adminId) {
            if (CacheHelper.IsExistCache("AdminMenuList")) {
                Dictionary<int, List<Model.AdminPageAction>> adminMenuListDic = CacheHelper.GetCache("AdminMenuList") as Dictionary<int, List<Model.AdminPageAction>>;
                if (adminMenuListDic[adminId] != null) {
                    List<Model.AdminPageAction> adminMenuList = adminMenuListDic[adminId];
                    if (adminMenuList == null) {
                        adminMenuList = EnterRepository.GetRepositoryEnter().GetPageMenuRepository.GetAdminShowPage(adminId);
                        adminMenuListDic.Add(adminId, adminMenuList);
                        CacheHelper.AddCache("AdminMenuList", adminMenuListDic, 2);
                    }
                    return adminMenuList;
                }
                else {
                    return adminMenuListDic[adminId];
                }
            }
            else {
                List<Model.AdminPageAction> adminMenuList = EnterRepository.GetRepositoryEnter().GetPageMenuRepository.GetAdminShowPage(adminId);
                Dictionary<int, List<Model.AdminPageAction>> adminMenuListDic = new Dictionary<int, List<Model.AdminPageAction>>();
                adminMenuListDic.Add(adminId, adminMenuList);
                CacheHelper.AddCache("AdminMenuList", adminMenuListDic, 2);
                return adminMenuList;
            }
            //if (CacheHelper.IsExistCache("AdminMenuList_"+adminId)) {
            //    return CacheHelper.GetCache("AdminMenuList_" + adminId) as List<Model.AdminPageAction>;
            //}
            //else {
            //    List<Model.AdminPageAction> adminMenuList = EnterRepository.GetRepositoryEnter().GetPageMenuRepository.GetAdminShowPage(adminId);
            //    CacheHelper.AddCache("AdminMenuList_" + adminId, adminMenuList, 1);
            //    return adminMenuList;
            //}
            //return EnterRepository.GetRepositoryEnter().GetPageMenuRepository.GetAdminShowPage(adminId);
        }
        /// <summary>
        /// 获取当前管理员可操作菜单列表（超级管理员另取）
        /// </summary>
        /// <returns></returns>
        public static List<Model.AdminPageAction> GetNowAdminMenu() {
            return GetAdminMenu(WebCookieHelper.GetAdminId(0));
        }

        /// <summary>
        /// 加载按钮列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.PageAction> LoadActionCodeList() {
            if (CacheHelper.IsExistCache("ActionCodeList")) {
                return CacheHelper.GetCache("ActionCodeList") as List<Model.PageAction>;
            }
            else {
                List<Model.PageAction> pageActionList = AuthorDesign.Web.App_Start.Common.EnterRepository.GetRepositoryEnter().GetPageActionRepository.LoadEntities().OrderBy(m => m.ActionLevel).ToList();
                CacheHelper.AddCache("ActionCodeList", pageActionList, 1);
                return pageActionList;
            }
        }
    }
}