using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AuthorDesign.Common {
    public class CookieHelper {
        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        /// <param name="cookieValue">cookie值</param>
        /// <param name="domain">作用域，为空就不写入作用域</param>
        public static void SetCookie(String cookieName, String cookieValue, string domain) {
            if (String.IsNullOrEmpty(cookieName) || String.IsNullOrEmpty(cookieValue)) return;
            if (HttpContext.Current != null) {
                HttpCookie cookie = new HttpCookie(cookieName, cookieValue);
                if (domain.Length > 0) {
                    cookie.Domain = domain;
                }
                cookie.HttpOnly = true;
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        /// <param name="cookieValue">cookie值</param>
        /// <param name="domain">作用域，为空就不写入作用域</param>
        /// <param name="day">有效时间</param>
        public static void SetCookie(String cookieName, String cookieValue, string domain, int day) {
            if (String.IsNullOrEmpty(cookieName) || String.IsNullOrEmpty(cookieValue)) return;
            if (HttpContext.Current != null) {
                HttpCookie cookie = new HttpCookie(cookieName, cookieValue);
                if (domain.Length > 0) {
                    cookie.Domain = domain;
                }
                cookie.HttpOnly = true;
                cookie.Expires = DateTime.Now.AddDays(day);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        /// <summary>
        /// 设置cookie过期
        /// </summary>
        /// <param name="cookieName">需要过期的cookie名称</param>
        public static void ExpireCookie(String cookieName) {
            if (String.IsNullOrEmpty(cookieName)) return;
            if (HttpContext.Current != null) {
                HttpCookie cookie = new HttpCookie(cookieName, string.Empty);
                cookie.HttpOnly = true;
                cookie.Expires = DateTime.Now.AddYears(-5);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        /// <summary>
        /// 获取对应Cookie名称的值
        /// </summary>
        /// <param name="cookieName">Cookie 的名称</param>
        /// <returns></returns>
        public static string GetCookie(string cookieName) {
            if (string.IsNullOrEmpty(cookieName)) return string.Empty;
            if (System.Web.HttpContext.Current == null) return string.Empty;
            if (System.Web.HttpContext.Current.Request.Cookies[cookieName] == null) return string.Empty;
            else return System.Web.HttpContext.Current.Request.Cookies[cookieName].Value;
        }
        /// <summary>
        /// 判断对应的Cookie是否存在
        /// </summary>
        /// <param name="cookieName">Cookie 的名称</param>
        /// <returns></returns>
        public static bool ExistCookie(string cookieName) {

            if (string.IsNullOrEmpty(cookieName) || System.Web.HttpContext.Current == null) return false;
            if (System.Web.HttpContext.Current.Request.Cookies[cookieName] == null) return false;
            if (System.Web.HttpContext.Current.Request.Cookies[cookieName].Value == null) return false;
            return (System.Web.HttpContext.Current.Request.Cookies[cookieName].Value.Length > 0);
        }
    }
}
