using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorDesign.Common {
    /// <summary>
    /// 缓存帮助
    /// </summary>
    public class CacheHelper {
        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        /// <param name="key">需要判断的缓存名称</param>
        /// <returns></returns>
        public static bool IsExistCache(string key) {
            if (string.IsNullOrEmpty(key)) return false;
            if (System.Web.HttpRuntime.Cache[key] == null) return false;
            return true;
        }
        /// <summary>
        /// 读取缓存
        /// </summary>
        public static object GetCache(string key) {   ///检测对象是否为空
            if (System.Web.HttpRuntime.Cache == null) return null;
            if (string.IsNullOrEmpty(key)) return null;
            ///获取数据
            return System.Web.HttpRuntime.Cache[key];
        }
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存名称</param>
        /// <param name="obj">缓存对象</param>
        /// <param name="hour">有效时间</param>
        public static void AddCache(string key, object obj,int hour) {
            if (string.IsNullOrEmpty(key)) return;
            if (System.Web.HttpRuntime.Cache[key] == null) {
                System.Web.HttpRuntime.Cache.Insert(key, obj, null, DateTime.Now.AddHours(hour), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            else {
                System.Web.HttpRuntime.Cache.Remove(key);
                System.Web.HttpRuntime.Cache.Insert(key, obj, null, DateTime.Now.AddHours(hour), System.Web.Caching.Cache.NoSlidingExpiration);
            }
        }
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">需要移除的缓存</param>
        public static void RemoveCache(string key) {
            if (string.IsNullOrEmpty(key)) return;
            if (System.Web.HttpRuntime.Cache[key] != null) {
                System.Web.HttpRuntime.Cache.Remove(key);
            }
            return;
        }
    }
}
