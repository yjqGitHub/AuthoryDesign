using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthorDesign.Web.App_Start.Common {
    public class WebCookieHelper {
        #region 管理员cookie帮助
        public const string adminCookieName = "AuthorDesignAdminCookie";
        /// <summary>
        /// 设置管理员的信息
        /// </summary>
        /// <param name="adminId">管理员Id</param>
        /// <param name="adminUserName">管理员的用户名</param>
        /// <param name="adminLastLoginTime">上次登录时间</param>
        /// <param name="adminLastLoginIP">上次登录IP</param>
        /// <param name="adminLastLoginAddress">上次登录地址</param>
        /// <param name="isSurperAdmin">是否超级管理员</param>
        /// <param name="authoryId">角色Id</param>
        /// <param name="day">cookie有效时间</param>
        public static void SetCookie(int adminId, string adminUserName,DateTime adminLastLoginTime, string adminLastLoginIP, string adminLastLoginAddress, int isSurperAdmin, int authoryId,int day) {
            string cookieValue = string.Join("|*&^%$#@!", adminId, HttpUtility.UrlEncode(adminUserName, System.Text.Encoding.UTF8),  adminLastLoginTime, adminLastLoginIP, HttpUtility.UrlEncode(adminLastLoginAddress, System.Text.Encoding.UTF8), isSurperAdmin,authoryId);
            if (day == 0) {
                AuthorDesign.Common.CookieHelper.SetCookie(adminCookieName, cookieValue, "");
            }
            else {
                AuthorDesign.Common.CookieHelper.SetCookie(adminCookieName, cookieValue, "", day);
            }
        }
        /// <summary>
        /// 获取管理员基本信息
        /// </summary>
        /// <param name="index">【0：adminId，1：adminUserName，2：adminLastLoginTime，3：adminLastLoginIP，4：adminLastLoginAddress,5:超级管理员,6:角色Id】</param>
        /// <returns></returns>
        public static string GetAdminInfo(int index) {
            string value = string.Empty;
            string cookieValue = AuthorDesign.Common.CookieHelper.GetCookie(adminCookieName);
            if (!string.IsNullOrEmpty(cookieValue)) {
                string[] adminInfo = cookieValue.Split(new string[] { "|*&^%$#@!" }, StringSplitOptions.None);
                if (adminInfo.Length >= index) {
                    if (index == 1 || index == 4) {
                        value = HttpUtility.UrlDecode(adminInfo[index], System.Text.Encoding.UTF8);
                    }
                    else {
                        value = adminInfo[index];
                    }
                }
            }
            return value;
        }
        /// <summary>
        /// 获取管理员Id或者角色Id或者是否超级管理员
        /// </summary>
        /// <param name="index">[0:管理员Id;5:超级管理员;6:角色Id]</param>
        /// <returns></returns>
        public static int GetAdminId(int index) {
            string adminId = GetAdminInfo(index);
            return string.IsNullOrEmpty(adminId) ? 0 : int.Parse(adminId);
        }
        /// <summary>
        /// 判断管理员是否登陆
        /// </summary>
        /// <returns></returns>
        public static bool AdminCheckLogin() {
            if (AuthorDesign.Common.CookieHelper.ExistCookie(adminCookieName)) {
                return true;
            }
            else {
                return false;
            }
        }
        /// <summary>
        /// 注销管理员Cookie
        /// </summary>
        public static void AdminLoginOut() {
            if (AuthorDesign.Common.CookieHelper.ExistCookie(adminCookieName)) {
                AuthorDesign.Common.CookieHelper.ExpireCookie(adminCookieName);
            }
        }
        #endregion
    }
}