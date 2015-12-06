using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AuthorDesign.Web.App_Start.Common {
    public class PublicFunction {
        /// <summary>
        /// 添加操作记录（不与数据库进行交互）
        /// </summary>
        /// <param name="actionType">操作动作类型（1：增，2：修改，3：查看，4：删除）</param>
        /// <param name="title">操作标题</param>
        /// <param name="content">操作内容</param>
        public static void AddOperation(int actionType, string title, string content) {
            AuthorDesign.Model.AdminOperation operation = new Model.AdminOperation() {
                Action = actionType,
                AdminId = WebCookieHelper.GetAdminId(0),
                AuthoryId = WebCookieHelper.GetAdminId(6),
                Content = content,
                CreateTime = DateTime.Now,
                IsSuperAdmin = (byte)WebCookieHelper.GetAdminId(5),
                Title = title,
                OperateIP = IpHelper.GetRealIP(),
                OperateInfo = IpHelper.GetBrowerVersion()
            };
            operation.OperateAddress = IpHelper.GetAdrByIp(operation.OperateIP);
            EnterRepository.GetRepositoryEnter().GetAdminOperationRepository.AddEntity(operation);
        }

        /// <summary>
        /// 获得指定数量的htmlSpan元素
        /// </summary>
        /// <returns></returns>
        public static string GetHtmlSpan(int count) {
            if (count <= 0)
                return "";

            if (count == 1)
                return "<span></span>";
            else if (count == 2)
                return "<span></span><span></span>";
            else if (count == 3)
                return "<span></span><span></span><span></span>";
            else {
                StringBuilder result = new StringBuilder();

                for (int i = 0; i < count; i++)
                    result.Append("<span></span>");

                return result.ToString();
            }
        }
        /// <summary>
        /// 获得指定数量的html空格
        /// </summary>
        /// <returns></returns>
        public static string GetHtmlBS(int count) {
            if (count == 1)
                return "&nbsp;&nbsp;";
            else if (count == 2)
                return "&nbsp;&nbsp;&nbsp;&nbsp;";
            else if (count == 3)
                return "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            else {
                StringBuilder result = new StringBuilder();

                for (int i = 0; i < count; i++)
                    result.Append("");

                return result.ToString();
            }
        }
        /// <summary>
        /// 判断同逗号隔开的数字列表中是否包含某个数字
        /// </summary>
        /// <param name="numList">同逗号隔开的数字列表</param>
        /// <param name="num">要判断的数字</param>
        /// <returns>包含则返回true 不包含则返回false</returns>
        public static bool IsContentNum(string numList, int num) {
            if (string.IsNullOrEmpty(numList)) return false;
            else {
                string[] ss= numList.Split(',');
                return IsContentNum(ss, num);
            }
            return false;
        }
        /// <summary>
        /// 判断字符串数组中是否包含某个数字
        /// </summary>
        /// <param name="ss">字符串数组</param>
        /// <param name="num">要判断的数字</param>
        /// <returns>包含则返回true 不包含则返回false</returns>
        public static bool IsContentNum(string[] ss, int num) {
            foreach (var item in ss) {
                if (num.ToString() == item) {
                    return true;
                }
            }
            return false;
        }
    }
}