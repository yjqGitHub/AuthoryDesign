using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthorDesign.Web.Areas.Admin.Models
{
    #region 分页基类
    /// <summary>
    /// 分页基类
    /// </summary>
    public class ParamModel
    {
        /// <summary>
        /// DataTable请求服务器端次数
        /// </summary>       
        public string sEcho { get; set; }

        /// <summary>
        /// 每页显示的数量
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// 分页时每页跨度数量
        /// </summary>
        public int iDisplayStart { get; set; }
    }
    #endregion

    #region 角色分页
    /// <summary>
    /// 角色分页
    /// </summary>
    public class RoleParamModel : ParamModel
    {
        /// <summary>
        /// 是否根据排序字段进行倒序排列
        /// </summary>
        public bool IsDesc { get; set; }
    }
    #endregion

    #region 按钮分页
    /// <summary>
    /// 按钮分页
    /// </summary>
    public class ButtonParamModel : ParamModel
    {
        public bool IsDesc { get; set; }
    }
    #endregion
    #region 管理员分页
    /// <summary>
    /// 管理员分页
    /// </summary>
    public class AdminParamModel:ParamModel
    {
        /// <summary>
        /// 权限Id
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 是否倒序排列
        /// </summary>
        public bool IsDesc { get; set; }
    }
    #endregion
}